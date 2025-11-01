using System.Data.Entity.Core;
using Domain.Component.Product;
using Domain.Component.Product.Attachments;
using Domain.Component.Product.CompletedSheath;
using Domain.Component.Product.Knife;
using Domain.Component.Product.Reviews;
using Infrastructure.Components.Products.Filters;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products;

public class ProductRepository : ComponentRepository<Product>, 
    IProductRepository,
    IGetProductPaginatedList,
    IAddReviewToProductRepository,
    IGetNotActiveProducts<Product>,
    IGetOldUnusedProducts<Product>
{
    public ProductRepository(DBContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetProductsByIds(List<Guid> ids)
    {
        List<Product> products = await EntityFrameworkQueryableExtensions.ToListAsync(this.Set.Where(p => ids.Contains(p.Id)));
        return products;
    }

    public async Task<PaginatedResult<Product>> GetProductPaginatedList(int pageNumber, int pageSize,
        string locale, ProductFilters? filters = null)
    {
        IQueryable<Product> productsQuery = this.Set
            .Include(p => p.Image)
            .Where(p => p.IsActive)
            .AsQueryable();

        if (filters != null)
        {
            if (filters.ProductType != null)
            {
                switch (filters.ProductType.ToLower())
                {
                    case "knife":
                        productsQuery = productsQuery.OfType<Knife>();
                        break;
                    case "sheath":
                        productsQuery = productsQuery.OfType<CompletedSheath>();
                        break;
                    case "attachment":
                        productsQuery = productsQuery.OfType<Attachment>();
                        break;
                }
            }

            if (filters.Styles is { Count: > 0 })
            {
                var productAfterQuery = this.Context.Set<Product>()
                    .FromSqlInterpolated(
                        $@"
                            SELECT DISTINCT ""p"".* FROM public.""Product"" as ""p""
                            INNER JOIN ""EngravingKnife"" as ""ek"" ON ""ek"".""KnifeId"" = ""p"".""Id""
                            INNER JOIN ""Engravings"" as ""eng"" ON ""ek"".""EngravingsId"" = ""eng"".""Id""
                            INNER JOIN ""Engraving_EngravingTag"" as ""eng_to_tags"" ON ""eng_to_tags"".""EngravingId"" = ""eng"".""Id""
                            INNER JOIN ""EngravingTags"" as ""eng_tags"" ON ""eng_to_tags"".""TagsId"" = ""eng_tags"".""Id""
                            WHERE ""p"".""IsActive"" = true
                            AND jsonb_extract_path_text(""eng_tags"".""Name_TranslationDictionary"", {locale}) = ANY ({filters.Styles})
                            
                           UNION

                            SELECT DISTINCT ""p"".* FROM public.""Product"" as ""p""
                            INNER JOIN ""CompletedSheathEngraving"" as ""comp_sheath_eng"" ON ""comp_sheath_eng"".""CompletedSheathId"" = ""p"".""Id""
                            INNER JOIN ""Engravings"" as ""eng"" ON ""comp_sheath_eng"".""EngravingsId"" = ""eng"".""Id""
                            INNER JOIN ""Engraving_EngravingTag"" as ""eng_to_tags"" ON ""eng_to_tags"".""EngravingId"" = ""eng"".""Id""
                            INNER JOIN ""EngravingTags"" as ""eng_tags"" ON ""eng_to_tags"".""TagsId"" = ""eng_tags"".""Id""
                            WHERE ""p"".""IsActive"" = true
                            AND jsonb_extract_path_text(""eng_tags"".""Name_TranslationDictionary"", {locale}) = ANY ({filters.Styles})
                        "
                    );
                
                var filteredIds = await productAfterQuery.Select(p => p.Id).ToListAsync();
                productsQuery = productsQuery.Where(p => filteredIds.Contains(p.Id));

            }
            if (filters.Colors is { Count: > 0 })
            {
                var productsColorQuery = this.Set
                    .FromSqlInterpolated(
                        $@"
                            SELECT DISTINCT ""p"".*
                            FROM public.""Product"" as ""p""
                            INNER JOIN ""Handles"" as ""h"" ON ""h"".""Id"" = ""p"".""HandleId""
                            INNER JOIN ""SheathColors"" as ""sh_col"" ON ""sh_col"".""Id"" = ""p"".""SheathColorId""
                            INNER JOIN ""BladeCoatingColors"" as ""coatings"" ON ""coatings"".""Id"" = ""p"".""ColorId""
                            WHERE ""p"".""IsActive"" = true
                            AND (
	                            jsonb_extract_path_text(""h"".""Color_TranslationDictionary"", {locale}) = ANY ({filters.Colors}) 
	                            OR jsonb_extract_path_text(""sh_col"".""Color_TranslationDictionary"", {locale}) = ANY ({filters.Colors}) 
	                            OR jsonb_extract_path_text(""coatings"".""Color_TranslationDictionary"", {locale}) = ANY ({filters.Colors})
	                            )

                            UNION 

                            SELECT DISTINCT ""p"".*
                            FROM public.""Product"" as ""p""
                            INNER JOIN ""SheathColors"" as ""sh_col"" ON ""sh_col"".""Id"" = ""p"".""CompletedSheath_SheathColorId""
                            WHERE ""p"".""IsActive"" = true
                            AND jsonb_extract_path_text(""sh_col"".""Color_TranslationDictionary"", {locale}) = ANY ({filters.Colors})
                        "
                    );

                var filteredIds = await productsColorQuery.Select(p => p.Id).ToListAsync();
                productsQuery = productsQuery.Where(p => filteredIds.Contains(p.Id));
            }
            if (filters.MinPrice.HasValue || filters.MaxPrice.HasValue)
            {
                var baseKnifeQuery = productsQuery.OfType<Knife>();
                var baseSheathQuery = productsQuery.OfType<CompletedSheath>();
                var baseAttachmentQuery = productsQuery.OfType<Attachment>();
                
                if (filters.MinPrice.HasValue)
                {
                    baseKnifeQuery = baseKnifeQuery.Where(knife => knife.TotalPriceInUah >= filters.MinPrice.Value);
                    baseSheathQuery = baseSheathQuery.Where(sheath => sheath.TotalPriceInUah >= filters.MinPrice.Value);
                    baseAttachmentQuery = baseAttachmentQuery.Where(attachment => attachment.Price >= filters.MinPrice.Value);
                }

                if (filters.MaxPrice.HasValue)
                {
                    baseKnifeQuery = baseKnifeQuery.Where(knife => knife.TotalPriceInUah <= filters.MaxPrice.Value);
                    baseSheathQuery = baseSheathQuery.Where(sheath => sheath.TotalPriceInUah <= filters.MaxPrice.Value);
                    baseAttachmentQuery = baseAttachmentQuery.Where(attachment => attachment.Price <= filters.MaxPrice.Value);
                }

                var filteredKnivesIds = await baseKnifeQuery.Select(p => p.Id).ToListAsync();
                var filteredSheathIds = await baseSheathQuery.Select(p => p.Id).ToListAsync();
                var filteredAttachmentIds = await baseAttachmentQuery.Select(p => p.Id).ToListAsync();
                
                productsQuery = productsQuery.Where(
                    p => filteredKnivesIds.Contains(p.Id) 
                         || filteredSheathIds.Contains(p.Id) 
                         || filteredAttachmentIds.Contains(p.Id)
                );
            }

            if (filters.MinBladeLength != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.BladeLength >= filters.MinBladeLength);
            }

            if (filters.MaxBladeLength != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.BladeLength <= filters.MaxBladeLength);
            }

            if (filters.MinTotalLength != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.TotalLength >= filters.MinTotalLength);
            }

            if (filters.MaxTotalLength != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.TotalLength <= filters.MaxTotalLength);
            }

            if (filters.MinBladeWidth != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.BladeWidth >= filters.MinBladeWidth);
            }

            if (filters.MaxBladeWidth != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.BladeWidth <= filters.MaxBladeWidth);
            }

            if (filters.MinBladeWeight != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.BladeWeight >= filters.MinBladeWeight);
            }

            if (filters.MaxBladeWeight != null)
            {
                productsQuery = productsQuery.OfType<Knife>()
                    .Where(knife => knife.Blade.BladeCharacteristics.BladeWeight <= filters.MaxBladeWeight);
            }
        }

        int totalProductsCount = await productsQuery.CountAsync();

        List<Product> productsList = await productsQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        foreach (var product in productsList)
        {
            switch (product)
            {
                case Knife knife:
                    await this.Context.Entry(knife).Reference(k => k.Blade).LoadAsync();
                    break;
            }
        }

        return new PaginatedResult<Product>
        {
            Items = productsList,
            Page = pageNumber,
            PageSize = pageSize,
            TotalItems = totalProductsCount
        };
    }

    public async Task AddReview(Guid productId, Review review)
    {
        Product existingProduct = await this.GetById(productId);
        existingProduct.AddReview(review);
        await this.Context.SaveChangesAsync();  
    }

    public override async Task<Product> GetById(Guid id)
    {
        return await this.Set
                   .Include(p => p.Image)
                   .Include(p => p.Reviews)
                   .Include(p => (p as Knife).Blade)
                   .FirstOrDefaultAsync(p => p.Id == id) 
               ?? throw new ObjectNotFoundException("Product not found");
    }

    public async Task<List<Product>> GetNotActiveProducts()
    {
        return await this.Set
            .Where(p => !p.IsActive)
            .ToListAsync();
    }

    public async Task<List<Product>> GetOldUnusedProducts()
    {
        return await this.Set
            .Where(p => !p.IsActive && (DateTime.UtcNow - p.CreatedAt).TotalDays > 30)
            .ToListAsync();
    }
    
    public async Task<List<Guid>> GetOldUnusedIds()
    {
        return await this.Set
            .Where(p => !p.IsActive && (DateTime.UtcNow - p.CreatedAt).TotalDays > 30)
            .Select(p => p.Id)
            .ToListAsync();
    }
}