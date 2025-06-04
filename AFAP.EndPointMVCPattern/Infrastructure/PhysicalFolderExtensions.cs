namespace AFAP.EndPointMVCPattern.Infrastructure
{
    public  class PhysicalFolderExtensions: IPhysicalFolderExtensions
    {  
        public string GetPhysicalFolder()
        {
            string externalStaticFolderPath = Path.Combine("/ExternalStaticFiles");
            return externalStaticFolderPath;
        }

    }
}
