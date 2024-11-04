using DesignPatterns.Creational.Singleton;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatternsTest.Creational;

[TestClass]
public class SingletonTests
{
    private string _expectedSettingKey = "expectedKey";
    private int _expectedSettingValue = 23;

    [TestInitialize]
    public void TestInitialize()
    {
        AppSettings.GetInstance().SetSetting(_expectedSettingKey, _expectedSettingValue);
    }

    [TestMethod]
    public void AppSettings_GetSetting_ReturnsExpectedValue()
    {
        // Act
        int actualValue = AppSettings.GetInstance().GetSetting<int>(_expectedSettingKey);

        // Assert
        Assert.AreEqual(_expectedSettingValue, actualValue);
    }

    [TestMethod]
    public void AppSettings_GetSetting_ReturnsDefaultIfNoKey()
    {
        // Act
        int actualValue = AppSettings.GetInstance().GetSetting<int>("noKey");

        // Assert
        Assert.AreEqual(0, actualValue);
    }
}