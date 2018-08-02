using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musili.WebApi.Models;

namespace Musili.Tests
{
    [TestClass]
    public class TracksCriteriaSetTest
    {
        [TestMethod]
        public void TestEmptyLists() {
            var criteriaSet = new TracksCriteriaSet("", "");
            Assert.AreEqual(Tempo.Any, criteriaSet.GetRandomCriteria().Tempo);
            Assert.AreEqual(Genre.Any, criteriaSet.GetRandomCriteria().Genre);
        }

        [TestMethod]
        public void TestNotEmptyListsWithBadValues() {
            var criteriaSet = new TracksCriteriaSet("sadsadsad,soft", "rock,");
            var criteria = criteriaSet.GetRandomCriteria();
            Assert.AreEqual(Tempo.Soft, criteria.Tempo);
            Assert.AreEqual(Genre.Rock, criteria.Genre);
        }
    }
}
