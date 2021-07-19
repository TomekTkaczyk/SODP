using SODP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Model
{
    public class ProjectTests
    {
        [Theory]
        [InlineData("1101PB_SomeName_Project")]
        [InlineData("1101_SomeName_Project_PB")]

        //name conventions
        // public void UnitOfWork_StateUnderTest_ExpectedBehavior()

        public void New_project_created_from_foldername_has_valid_stage_PB(string foldername)
        {
            var project = new Project(foldername);
            var expected = new Project("1101", "PB", "SomeName_Project");

            Assert.Equal(expected.Symbol, project.Symbol);
            // Assert.Equal(expected.Title, project.Title);
        }
    }
}
