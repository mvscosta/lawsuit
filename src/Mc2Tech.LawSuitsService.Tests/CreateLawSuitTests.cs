using System;
using Xunit;

namespace Mc2Tech.LawSuitsService.Tests
{
    public class CreateLawSuitTests
    {
        [Fact]
        public void CreateLawSuit_WithoutUnifiedProcessNumber_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_WithUnifiedProcessNumberDiff20Chars_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_DuplicatedUnifiedProcessNumber_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_DistributedDateBiggerThanToday_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_ClientPhysicalFolderBiggerThan50Chars_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_DescriptionBiggerThan1000Chars_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_WithoutSituation_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_WithoutResponsible_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_WithDuplicatedResponsible_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_WithMoreThan3Responsible_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_CircularReferenceHierarchy_ValidationError()
        {

        }

        [Fact]
        public void CreateLawSuit_HierararchyBiggerThan4_ValidationError()
        {

        }
    }
}
