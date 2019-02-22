using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Musili.WebApi.Models;

namespace Musili.Tests
{
    [TestClass]
    public class TracksCriteriaTest
    {
        [TestMethod]
        public void TestParse() {
            var criteriaSet = new TracksCriteria("", "");
            Assert.AreEqual(0, criteriaSet.Tempos.Count);
            Assert.AreEqual(0, criteriaSet.Genres.Count);

            criteriaSet = new TracksCriteria("any", "any");
            Assert.AreEqual(0, criteriaSet.Tempos.Count);
            Assert.AreEqual(0, criteriaSet.Genres.Count);

            criteriaSet = new TracksCriteria("sadsad", "asdsad,asd");
            Assert.AreEqual(0, criteriaSet.Tempos.Count);            
            Assert.AreEqual(0, criteriaSet.Genres.Count);

            criteriaSet = new TracksCriteria("soft", "rock,metal");
            Assert.AreEqual(1, criteriaSet.Tempos.Count);
            Assert.AreEqual(2, criteriaSet.Genres.Count);                        

            criteriaSet = new TracksCriteria("soft,rhytmic,any", "rock,metal,classical,jazz,electronic");
            Assert.AreEqual(2, criteriaSet.Tempos.Count);
            Assert.AreEqual(5, criteriaSet.Genres.Count);
        }

        [TestMethod]
        public void TestIsAnyTempo() {
            var criteriaSet = new TracksCriteria("any", "rock,");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteria("", "");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteria("asdsa", "");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteria("soft", "");
            Assert.AreEqual(false, criteriaSet.IsAnyTempo);

            criteriaSet = new TracksCriteria("soft,rhytmic", "");
            Assert.AreEqual(true, criteriaSet.IsAnyTempo);
        }

        [TestMethod]
        public void TestIsAnyGenre() {
            var criteriaSet = new TracksCriteria("any", "any");
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteria("", "");
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteria("soft", "asdsdad");
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteria("soft", "rock");
            Assert.AreEqual(false, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteria("soft,rhytmic", "rock,jazz");
            Assert.AreEqual(false, criteriaSet.IsAnyGenre);

            criteriaSet = new TracksCriteria("soft,rhytmic", String.Join(',', Enum.GetNames(typeof(Genre)).Where(item => item != "Any").ToArray()));
            Assert.AreEqual(true, criteriaSet.IsAnyGenre);            
        }
    }
}
