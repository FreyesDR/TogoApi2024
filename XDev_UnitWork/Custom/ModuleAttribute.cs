namespace XDev_UnitWork.Custom
{
    public class ModuleAttribute : Attribute 
    {
        public ModuleAttribute(string name)
        {
            Module = name;
        }
        public string Module { get; }
        
    }
}
