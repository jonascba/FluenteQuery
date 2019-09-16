using FluentAssertions;
using Xunit;

namespace FluentQuery.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_create_SELECT_instruction()
        {
            var sampleEntity1 = new SampleEntity1();
            
            var fluentQuery = new FluentQuery();
            var select = fluentQuery.Select(sampleEntity1.Id, sampleEntity1.Name);

            select.ToString().Should().Be(" SELECT  SampleEntityOne.Id , SampleEntityOne.Name  \r\n");
        }
        
        [Fact]
        public void Should_create_FROM_instruction()
        {
            var sampleEntity1 = new SampleEntity1();
            
            var fluentQuery = new FluentQuery();
            var select = fluentQuery.From(sampleEntity1);

            select.ToString().Should().Be("  FROM SampleEntityOne \r\n");
        }
    }

    internal class SampleEntity1
    {
        private static string EntityName => "SampleEntityOne";
        public  EntityField Id => new EntityField(EntityName, "Id");
        public  EntityField Name => new EntityField(EntityName, "Name");

        public override string ToString()
        {
            return EntityName;
        }
    }
}