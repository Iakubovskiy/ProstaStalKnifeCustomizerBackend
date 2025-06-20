using System.Data.Entity.Core;
using Domain.Component.Product.Attachments;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Products.Attachments;

public class AttachmentRepository : ComponentRepository<Attachment>
{
    public AttachmentRepository(DBContext context)
        : base(context)
    {
        
    }

    public override async Task<List<Attachment>> GetAll()
    {
        return await this.Set
            .Include(product => product.Tags)
            .Include(attachment => attachment.Model)
            .Include(attachment => attachment.Image)
            .Include(attachment => attachment.Type)
            .ToListAsync();
    }
    
    public override async Task<List<Attachment>> GetAllActive()
    {
        return await this.Set
            .Where(product => product.IsActive)
            .Include(product => product.Tags)
            .Include(attachment => attachment.Model)
            .Include(product => product.Image)
            .Include(product => product.Type)
            .ToListAsync();
    }

    public override async Task<Attachment> GetById(Guid id)
    {
        return await this.Set
                   .Include(product => product.Tags)
                   .Include(product => product.Reviews)
                   .Include(product => product.Model)
                   .Include(product => product.Image)
                   .Include(product => product.Type)
                   .FirstOrDefaultAsync(product => product.Id == id)
               ?? throw new ObjectNotFoundException("Entity not found");
    }
}