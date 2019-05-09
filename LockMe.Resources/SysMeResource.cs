namespace LockMe.Resources
{
    public class SysMeResource
    {
        public static  object GetResourceObject(string name)
        {
            return MeResource.ResourceManager.GetObject(name);
        }
    }
}