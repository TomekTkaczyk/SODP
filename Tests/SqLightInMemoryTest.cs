using SODP.Application.Validators;
using SODP.Model;
using System;
using Xunit;

namespace Tests
{
    public class SqLightInMemoryTest : ServiceTest<AppDictionary>, IDisposable
    {
        public SqLightInMemoryTest() : base(new DictionaryValidator()) { }

        [Fact]
        public void when_add_AppDictionary_entity_to_sqlight_memory_database_ActiveStatus_should_by_false()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var dictionary = new AppDictionary()
            {
                Sign = "PARTS",
                Name = "CZĘŚCI PROJEKTU",
                ActiveStatus = false
            };

            var entity = _context.AppDictionary.Add(dictionary);
            _context.SaveChanges();

            Assert.False(entity.Entity.ActiveStatus);
        }

        [Fact]
        public void when_add_AppDictionary_entity_to_sqlight_memory_database_ActiveStatus_should_by_true()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            var dictionary = new AppDictionary()
            {
                Sign = "PARTS",
                Name = "CZĘŚCI PROJEKTU",
                ActiveStatus = true
            };

            var entity = _context.AppDictionary.Add(dictionary);
            _context.SaveChanges();

            Assert.True(entity.Entity.ActiveStatus);
        }
    }
}
