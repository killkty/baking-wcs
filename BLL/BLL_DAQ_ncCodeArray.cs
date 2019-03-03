using GlobalsInfo;
using Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BLL
{
    public class BLL_DAQ_ncCodeArray
    {
        public List<Model_DAQ_ncCodeArray> GetAllContacts()
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

                IEnumerable<XElement> xmlContacts = xmlDocument.Element("Models")
                    .Elements("Model");

                List<Model_DAQ_ncCodeArray> contacts = new List<Model_DAQ_ncCodeArray>();

                //获取 列表
                foreach (XElement i in xmlContacts)
                {
                    Model_DAQ_ncCodeArray contact = new Model_DAQ_ncCodeArray();

                    contact.Id = int.Parse(i.Attribute("Id").Value);
                    contact.ncCode = i.Element("ncCode").Value;
                    contact.trayHasNc = i.Element("trayHasNc").Value;
                    contacts.Add(contact);
                }

                return contacts;
            }
        }

        public Model_DAQ_ncCodeArray GetContactById(int id)
        {
            XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

            XElement xmlContact = xmlDocument.Element("Models").Elements("Model")
                .Where(a => a.Attribute("Id").Value == id.ToString()).FirstOrDefault();

            Model_DAQ_ncCodeArray contact = new Model_DAQ_ncCodeArray();

            contact.Id = int.Parse(xmlContact.Attribute("Id").Value);
            contact.ncCode = xmlContact.Element("ncCode").Value;
            contact.trayHasNc = xmlContact.Element("trayHasNc").Value;
            return contact;
        }

        public void AddContact(Model_DAQ_ncCodeArray contact)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

                //将值插入 xml文档
                xmlDocument.Element("Models").Add(
                    new XElement("Model",
                        new XAttribute("Id", contact.Id),
                        new XElement("ncCode", contact.ncCode),
                        new XElement("trayHasNc", contact.trayHasNc)
                        ));

                xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");
            }
        }

        public void UpdateContact(Model_DAQ_ncCodeArray contact)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

                //通过ID更新
                XElement xmlContact = xmlDocument.Element("Models").Elements("Model")
                    .Where(x => x.Attribute("Id").Value == contact.Id.ToString())
                    .FirstOrDefault();

                //更新
                xmlContact.SetElementValue("ncCode", contact.ncCode);
                xmlContact.SetElementValue("trayHasNc", contact.trayHasNc);

                xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");
            }
        }

        public void DeleteContact(int id)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

                //得到 并删除他
                xmlDocument.Root.Elements().Where(x => x.Attribute("Id").Value == id.ToString())
                    .FirstOrDefault().Remove();

                xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");
            }
        }

        public int GetNextId()
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                int currentId;
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

                //获取id的集合
                IEnumerable<int> contactIds = xmlDocument.Element("Models").Elements("Model")
                    .Select(x => int.Parse(x.Attribute("Id").Value));

                //如果集合为空，则将变量设置为0，否则将该变量设置为最高ID值
                if (contactIds.Count() == 0)
                    currentId = 0;
                else
                    currentId = contactIds.Max();

                return currentId + 1;
            }
        }

        public void SetXmlFile()
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                //如果xml文件丢失，则使用根元素创建它
                if (!(File.Exists(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml")))
                {
                    XDocument xmlDocument = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Models"));

                    xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");
                }
            }
        }

        public bool IdExists(int id)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_DAQ_ncCodeArray.xml");

                return xmlDocument.Element("Models").Elements("Model")
                    .Select(x => int.Parse(x.Attribute("Id").Value))
                    .Where(y => y == id).Count() > 0;
            }
        }

    }
}
