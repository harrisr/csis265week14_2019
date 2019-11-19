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
    public class TestAuthorDAO
    {
        private static readonly ILog logger =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [OneTimeSetUp]
        public void SetupForAllTests()
        {
            log4net.Config.XmlConfigurator.Configure();
        }



        [Test]
        public void TestAuthorDAOOneArgConstructor()
        {
            logger.Debug("INSIDE  TestAuthorDAOOneArgConstructor()!!!!!!!!!!!!!");
            try
            {
                AuthorDAO dao = new AuthorDAO("localhost");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false);
            }
        }




        [Test]
        public void TestAuthorDAOSelectOne()
        {
            logger.Debug("INSIDE  TestAuthorDAOSelectOne()!!!!!!!!!!!!!");
            try
            {
                AuthorDAO dao = new AuthorDAO("localhost");

                Author filter = new Author(2, "BLANK", "BLANK", DateTime.Now);

                Author result = (Author)dao.SelectOneObject(filter);
                logger.Debug(result.ToString());
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.GetId());
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }



        [Test]
        public void TestAuthorDAOInsertOne()
        {
            logger.Debug("INSIDE  TestAuthorDAOInsertOne()!!!!!!!!!!!!!");
            try
            {
                AuthorDAO dao = new AuthorDAO("localhost");

                Author newAuthor = new Author(-1, "Asimov", "isaac@asimov.com", DateTime.Now);

                logger.Debug($"BEFORE INSERT: {newAuthor.ToString()}");

                Author result = (Author)dao.InsertOneObject(newAuthor);

                logger.Debug($"AFTER INSERT: {result.ToString()}");

                Assert.IsNotNull(result);
                Assert.IsTrue(result.GetId() > 0);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Assert.IsTrue(false);
            }
        }



        [Test]
        public void TestAuthorDAOSelectMany()
        {
            logger.Debug("INSIDE  TestAuthorDAOSelectMany()!!!!!!!!!!!!!");
            try
            {
                AuthorDAO dao = new AuthorDAO("localhost");

                Author filter = new Author(-1, "%", "BLANK", DateTime.Now);

                //Author result = (Author)dao.SelectOneObject(filter);

                IList<object> objList = dao.SelectManyObjects(filter);
                Assert.IsNotNull(objList);
                Assert.IsTrue(objList.Count >= 1);

                foreach (Author temp in objList)
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
        public void TestAuthorDAOUpdateOne()
        {
            logger.Debug("INSIDE  TestAuthorDAOUpdateOne()!!!!!!!!!!!!!");
            try
            {
                AuthorDAO dao = new AuthorDAO("localhost");
                Author filter = new Author(2, "AsimovZZ", "isaacZZ@asimov.com", DateTime.Now);
                Author result = (Author)dao.SelectOneObject(filter);

                logger.Debug($"BEFORE UPDATE: {result.ToString()}");

                string newName = result.GetName();
                newName += "ZZZ";
                result.SetName(newName);

                Author temp = (Author)dao.UpdateOneObject(result);

                Author result2 = (Author)dao.SelectOneObject(filter);

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
        public void TestAuthorDAODeleteOne()
        {
            logger.Debug("INSIDE  TestAuthorDAODeleteOne()!!!!!!!!!!!!!");
            try
            {
                AuthorDAO dao = new AuthorDAO("localhost");
                Author filter = new Author(-1, "%", "BLANK", DateTime.Now);

                IList<object> objList = dao.SelectManyObjects(filter);
                int count1 = objList.Count;
                logger.Debug($"BEFORE DELETE: {count1}");

                Author temp = new Author(2, "e", "BLANK", DateTime.Now);

                Author delete = (Author)dao.DeleteOneObject(temp);

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
