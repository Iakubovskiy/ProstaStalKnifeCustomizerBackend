using Domain.Component.BladeShapes.BladeShapeTypes;

namespace Infrastructure.Data.Component.BladeShapes;

public class BladeShapeTypeSeeder : ISeeder
{
    public int Priority => 0;
    private readonly IRepository<BladeShapeType> _bladeShapeTypeRepository;
    
    public BladeShapeTypeSeeder(IRepository<BladeShapeType> bladeShapeTypeRepository)
    {
        this._bladeShapeTypeRepository = bladeShapeTypeRepository;
    }
    
    public async Task SeedAsync()
    {
        int count = (await this._bladeShapeTypeRepository.GetAll()).Count;
        if (count > 0)
        {
            return;
        }
        
        BladeShapeType bladeShapeType1 = new BladeShapeType(
            new Guid("1a2b3c4d-5e6f-4789-a012-3456789abcde"),
            "Drop Point"
        );
        
        BladeShapeType bladeShapeType2 = new BladeShapeType(
            new Guid("2b3c4d5e-6f7a-4890-b123-456789abcdef"),
            "Clip Point"
        );
        
        BladeShapeType bladeShapeType3 = new BladeShapeType(
            new Guid("3c4d5e6f-7a8b-4901-c234-56789abcdef0"),
            "Tanto"
        );
        
        BladeShapeType bladeShapeType4 = new BladeShapeType(
            new Guid("4d5e6f7a-8b9c-4012-d345-6789abcdef01"),
            "Spear Point"
        );
        
        BladeShapeType bladeShapeType5 = new BladeShapeType(
            new Guid("5e6f7a8b-9c0d-4123-e456-789abcdef012"),
            "Sheepsfoot"
        );
        
        BladeShapeType bladeShapeType6 = new BladeShapeType(
            new Guid("6f7a8b9c-0d1e-4234-f567-89abcdef0123"),
            "Wharncliffe"
        );
        
        BladeShapeType bladeShapeType7 = new BladeShapeType(
            new Guid("7a8b9c0d-1e2f-4345-0678-9abcdef01234"),
            "Straight Back"
        );
        
        BladeShapeType bladeShapeType8 = new BladeShapeType(
            new Guid("8b9c0d1e-2f3a-4456-1789-abcdef012345"),
            "Recurve"
        );
        
        BladeShapeType bladeShapeType9 = new BladeShapeType(
            new Guid("9c0d1e2f-3a4b-4567-289a-bcdef0123456"),
            "Hawkbill"
        );
        
        BladeShapeType bladeShapeType10 = new BladeShapeType(
            new Guid("0d1e2f3a-4b5c-4678-39ab-cdef01234567"),
            "Karambit"
        );
        
        await this._bladeShapeTypeRepository.Create(bladeShapeType1);
        await this._bladeShapeTypeRepository.Create(bladeShapeType2);
        await this._bladeShapeTypeRepository.Create(bladeShapeType3);
        await this._bladeShapeTypeRepository.Create(bladeShapeType4);
        await this._bladeShapeTypeRepository.Create(bladeShapeType5);
        await this._bladeShapeTypeRepository.Create(bladeShapeType6);
        await this._bladeShapeTypeRepository.Create(bladeShapeType7);
        await this._bladeShapeTypeRepository.Create(bladeShapeType8);
        await this._bladeShapeTypeRepository.Create(bladeShapeType9);
        await this._bladeShapeTypeRepository.Create(bladeShapeType10);
    }
}