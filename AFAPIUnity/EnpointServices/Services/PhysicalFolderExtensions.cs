using AFAPIUnity.EnpointServices.Contract;
namespace AFAPIUnity.EnpointServices.Services
{
    public  class PhysicalFolderExtensions: IPhysicalFolderExtensions
    {  
        public string GetPhysicalFolder()
        {
            string externalStaticFolderPath = Path.Combine("/Ex********************");
            return externalStaticFolderPath;
        }

    }
}
