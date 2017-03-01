using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class LabelingTests
    {
        [TestMethod]
        public void TestLabelPropertyExtractor()
        {
            // Using GUID values as random strings
            string random1 = Guid.NewGuid().ToString("D");
            string random2 = Guid.NewGuid().ToString("D");

            string propertyText = string.Empty;


            // Just text (create new label id)
            propertyText = $"@@@{random1.Replace('-', ' ')}";
            Assert.AreEqual(Labeling.LabelManager.getLabelId(propertyText), random1.Replace("-", string.Empty));

            // Label id and text (assuming that label id has no spaces)
            propertyText = $"@@@{random1.Replace("-", string.Empty)}={random2}";
            Assert.AreEqual(Labeling.LabelManager.getLabelId(propertyText), random1.Replace("-", string.Empty));
            Assert.AreEqual(Labeling.LabelManager.getLabel(propertyText), random2);

            // Label id and text (assuming that label id has spaces, which is wrong)
            propertyText = $"@@@{random1.Replace("-", " ")}={random2}";
            Assert.AreEqual(Labeling.LabelManager.getLabelId(propertyText), random1.Replace("-", string.Empty));
            Assert.AreEqual(Labeling.LabelManager.getLabel(propertyText), random2);
        }

        [TestMethod]
        public void TestLabelCreation()
        {
            // TODO At the moment this test is not working due the MetaModelService

            Labeling.LabelManager labelManger = new Labeling.LabelManager();
            Random random = new Random();

            string propertyText = string.Empty;

            // Using GUID values as random strings
            string randomText1;
            string randomtext2;

            for (int i = 0; i < random.Next(30, 300); i++)
            {
                randomText1 = Guid.NewGuid().ToString("D");
                randomtext2 = Guid.NewGuid().ToString("D");

                if ((i % 2) == 0)
                {
                    propertyText = $"@@@{randomText1.Replace("-", " ")}={randomtext2}";
                }
                else
                {
                    if ((i % 3) == 0)
                    {
                        propertyText = $"@@@{randomText1.Replace("-", string.Empty)}={randomtext2}";
                    }
                    else
                    {
                        propertyText = $"@@@{randomText1.Replace("-", " ")}";
                    }
                }


                labelManger.createLabel(propertyText);

                Assert.IsTrue(labelManger.exist(Labeling.LabelManager.getLabelId(propertyText), "LabelFileJYL_en-GB"));
            }
        }
    }
}
