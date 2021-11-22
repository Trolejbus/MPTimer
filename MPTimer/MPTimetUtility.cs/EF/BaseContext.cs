namespace MPTimetUtility.cs.EF
{
    public class BaseContext
    {
        public BaseContext()
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized +=
                (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
        }
    }
}
