using System.Collections.Generic;

using Xunit;

using CifFile.Lib;

namespace CifFile.Test
{
    public class ScheduleMatcherTest
    {
        IList<ScheduleCriteria> _criteria = new List<ScheduleCriteria>
        {
            new ScheduleCriteria("C12345", "P", "BRSTLTM", "STKGIEP"),
            new ScheduleCriteria("C23456", "O", "STKGIEP", "BRSTLTM")
        };

        [Fact]
        public void Match_Loose_Found_RetursTrue()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.True(sut.Match(_criteria, "C23456"));      
        }

        [Fact]
        public void Match_Loose_NotFound_RetursFalse()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.False(sut.Match(_criteria, "foo"));        
        }

        [Fact]
        public void Match_Loose_EmptyList_RetursTrue()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.True(sut.Match(new List<ScheduleCriteria>(), "C23456"));      
        }

        [Fact]
        public void Match_Loose_NullList_RetursTrue()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.True(sut.Match(null, "C23456"));      
        }

        [Fact]
        public void Match_Stricter_Found_RetursTrue()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.True(sut.Match(_criteria, "C23456", "O")); 
        }

        [Fact]
        public void Match_Stricter_NotFound_RetursFalse()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.False(sut.Match(_criteria, "C23456", "P"));          
        }

        [Fact]
        public void Match_Strictest_Found_RetursTrue()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.True(sut.Match(_criteria, "C23456", "O", "STKGIEP", "BRSTLTM")); 
        }

        [Fact]
        public void Match_Strictest_NotFound_RetursFalse()
        {
            ScheduleMatcher sut = new ScheduleMatcher();

            Assert.False(sut.Match(_criteria, "C23456", "O", "NPLEIEP", "BRSTLTM"));          
        }
    }
}