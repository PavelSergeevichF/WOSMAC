using System.IO;
using System.Xml;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    private string _path = "C:\\Users\\WOSMAC\\Save";
    public void SaveData(Vector3 Pos)
    {
        SavedData player = new SavedData();
        //player.Position(Pos.x,Pos.y, Pos.z);
        Save(player, _path);
    }
    public void LoadData()
    { }
    public void Save(SavedData player, string path = "")
    {
        var xmlDoc = new XmlDocument();

        XmlNode rootNode = xmlDoc.CreateElement("Player");
        xmlDoc.AppendChild(rootNode);

        var element = xmlDoc.CreateElement("Name");
        element.SetAttribute("value", player.Name);
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("PosX");
        element.SetAttribute("value", player.Position.X.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("PosY");
        element.SetAttribute("value", player.Position.Y.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("PosZ");
        element.SetAttribute("value", player.Position.Z.ToString());
        rootNode.AppendChild(element);
        element = xmlDoc.CreateElement("IsEnable");
        element.SetAttribute("value", player.IsEnabled.ToString());
        rootNode.AppendChild(element);

        XmlNode userNode = xmlDoc.CreateElement("Info");
        var attribute = xmlDoc.CreateAttribute("Unity");
        attribute.Value = Application.unityVersion;
        userNode.Attributes.Append(attribute);
        userNode.InnerText = "System Language: " +
                             Application.systemLanguage;
        rootNode.AppendChild(userNode);

        xmlDoc.Save(path);

    }
    public SavedData Load(string path = "")
    {
        var result = new SavedData();
        if (!File.Exists(path)) return result;
        using (var reader = new XmlTextReader(path))
        {
            while (reader.Read())
            {
                var key = "Name";
                if (reader.IsStartElement(key))
                {
                    result.Name = reader.GetAttribute("value");
                }
                key = "PosX";
                //if (reader.IsStartElement(key))
                //{
                //    result.Position.X = reader.GetAttribute("value").TrySingle();
                //}
                //key = "PosY";
                //if (reader.IsStartElement(key))
                //{
                //    result.Position.Y = reader.GetAttribute("value").TrySingle();
                //}
                //key = "PosZ";
                //if (reader.IsStartElement(key))
                //{
                //    result.Position.Z = reader.GetAttribute("value").TrySingle();
                //}
                //key = "IsEnable";
                //if (reader.IsStartElement(key))
                //{
                //    result.IsEnabled = reader.GetAttribute("value").TryBool();
                //}
            }
        }

        return result;
    }
}