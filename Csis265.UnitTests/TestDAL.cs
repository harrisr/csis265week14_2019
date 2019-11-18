using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Csis265.Domain;
using Csis265.DAL;
using log4net;

namespace Csis265.UnitTests
{
    [TestFixture]
    public class TestDAL
    {
        private static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        [OneTimeSetUp]
        public void SetupForAllTests()
        {
            log4net.Config.XmlConfigurator.Configure();
        }


        /*
        [Test]
        public void TestBaseDALDefaultConstructor()
        {
            logger.Debug("INSIDE  TestBaseDALDefaultConstructor()!!!!!!!!!!!!!");
            try
            {
                BaseDAO dao = new BaseDAO();
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [Test]
        public void TestBaseDALOneArgConstructor()
        {
            logger.Debug("INSIDE  TestBaseDALOneArgConstructor()!!!!!!!!!!!!!");
            try
            {
                BaseDAO dao = new BaseDAO("localhost");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }

        [Test]
        public void TestBaseDALBadConnectionKey()
        {
            logger.Debug("INSIDE  TestBaseDALBadConnectionKey()!!!!!!!!!!!!!");
            try
            {
                BaseDAO dao = new BaseDAO("localhostZZZZZ");
                Assert.IsTrue(false);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(true);
            }
        }

        [Test]
        public void TestBaseDALEmptyConnectionString()
        {
            logger.Debug("INSIDE  TestBaseDALEmptyConnectionString()!!!!!!!!!!!!!");
            try
            {
                BaseDAO dao = new BaseDAO("empty");
                Assert.IsTrue(false);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(true);
            }
        }
        */


        [Test]
        public void TestGenreDAOOneArgConstructor()
        {
            logger.Debug("INSIDE  TestGenreDAOOneArgConstructor()!!!!!!!!!!!!!");
            try
            {
                GenreDAO dao = new GenreDAO("localhost");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }




        [Test]
        public void TestGenreDAOSelectOne()
        {
            logger.Debug("INSIDE  TestGenreDAOSelectOne()!!!!!!!!!!!!!");
            try
            {
                GenreDAO dao = new GenreDAO("localhost");

                Genre filter = new Genre(3, "BLANK", DateTime.Now);

                Genre result = (Genre)dao.SelectOneObject(filter);
                logger.Debug(result.ToString());
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.GetId());
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }



        [Test]
        public void TestGenreDAOInsertOne()
        {
            logger.Debug("INSIDE  TestGenreDAOInsertOne()!!!!!!!!!!!!!");
            try
            {
                GenreDAO dao = new GenreDAO("localhost");

                Genre newGenre = new Genre(-1, "Western", DateTime.Now);

                logger.Debug( $"BEFORE INSERT: {newGenre.ToString()}");

                Genre result = (Genre)dao.InsertOneObject(newGenre);

                logger.Debug($"AFTER INSERT: {result.ToString()}");
                
                Assert.IsNotNull(result);
                Assert.IsTrue( result.GetId() > 0 );
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }



        [Test]
        public void TestGenreDAOSelectMany()
        {
            logger.Debug("INSIDE  TestGenreDAOSelectMany()!!!!!!!!!!!!!");
            try
            {
                GenreDAO dao = new GenreDAO("localhost");

                Genre filter = new Genre(-1, "e", DateTime.Now);

                //Genre result = (Genre)dao.SelectOneObject(filter);

                IList<object> objList = dao.SelectManyObjects(filter);
                Assert.IsNotNull(objList);
                Assert.IsTrue(objList.Count >= 1);

                foreach(Genre temp in objList)
                {
                    logger.Debug($"BACK FROM DB:  {temp.ToString()}");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }





        [Test]
        public void TestGenreDAOUpdateOne()
        {
            logger.Debug("INSIDE  TestGenreDAOUpdateOne()!!!!!!!!!!!!!");
            try
            {
                GenreDAO dao = new GenreDAO("localhost");
                Genre filter = new Genre(3, "BLANK", DateTime.Now);
                Genre result = (Genre)dao.SelectOneObject(filter);

                logger.Debug($"BEFORE UPDATE: {result.ToString()}");

                string newName = result.GetName();
                newName += "ZZZ";
                result.SetName(newName);

                Genre temp = (Genre)dao.UpdateOneObject(result);

                Genre result2 = (Genre)dao.SelectOneObject(filter);

                logger.Debug($"AFTER UPDATE: {result2.ToString()}");

                Assert.IsNotNull(result);
                Assert.AreEqual(newName, result2.GetName());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }



        [Test]
        public void TestGenreDAODeleteOne()
        {
            logger.Debug("INSIDE  TestGenreDAODeleteOne()!!!!!!!!!!!!!");
            try
            {
                GenreDAO dao = new GenreDAO("localhost");
                Genre filter = new Genre(3, "e", DateTime.Now);

                IList<object> objList = dao.SelectManyObjects(filter);
                int count1 = objList.Count;
                logger.Debug($"BEFORE DELETE: {count1}");

                Genre temp = new Genre(4, "e", DateTime.Now);

                Genre delete = (Genre)dao.DeleteOneObject(temp);

                IList<object> objList2 = dao.SelectManyObjects(filter);
                int count2 = objList2.Count;
                logger.Debug($"AFTER DELETE: {count2}");
                
                Assert.IsTrue(count2 == (count1 - 1));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }




    }
}
