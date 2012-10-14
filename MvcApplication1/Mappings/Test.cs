using FluentNHibernate.Mapping;
using MvcApplication1.Models;

namespace MvcApplication1.Mappings
{
    public class TestMap : ClassMap<Test>
    {
        public TestMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.AnotherProperty);
        }
    }
}