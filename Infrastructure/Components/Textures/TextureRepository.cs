using System.Data.Entity.Core;
using Domain.Component.Textures;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Components.Textures;

public class TextureRepository : BaseRepository<Texture>
{
    public TextureRepository(DBContext context) : base(context)
    {
    }

    public async override Task<List<Texture>> GetAll()
    {
        return await this.Context.Textures
                   .Include(texture => texture.NormalMap)
                   .Include(texture => texture.RoughnessMap)
                   .ToListAsync();
    }
    public async override Task<Texture> GetById(Guid id)
    {
        return await this.Context.Textures
           .Include(texture => texture.NormalMap)
           .Include(texture => texture.RoughnessMap)
           .FirstOrDefaultAsync(texture =>  texture.Id == id)
            ?? throw new ObjectNotFoundException("Entity not found");
    }
}