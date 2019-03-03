using GlobalsInfo;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL
{
    //class BLL_CWC_processLotArray
    //{
    //}

    public class BLL_CWC_processLotArray  // 数据采集
    {
        public List<Model_CWC_processLotArray> GetAllContacts()
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");//Contacts

                IEnumerable<XElement> xmlContacts = xmlDocument.Element("Models")
                    .Elements("Model");

                List<Model_CWC_processLotArray> contacts = new List<Model_CWC_processLotArray>();

                //获取列表
                foreach (XElement i in xmlContacts)
                {
                    Model_CWC_processLotArray contact = new Model_CWC_processLotArray();

                    contact.Id = int.Parse(i.Attribute("Id").Value);
                    contact.name = i.Element("name").Value;
                    contact.dataType = i.Element("dataType").Value;
                    contact.trayValue = i.Element("trayValue").Value;
                    contacts.Add(contact);
                }

                return contacts;
            }
        }

        public Model_CWC_processLotArray GetContactById(int id)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");

                XElement xmlContact = xmlDocument.Element("Models").Elements("Model")
                    .Where(a => a.Attribute("Id").Value == id.ToString()).FirstOrDefault();

                Model_CWC_processLotArray contact = new Model_CWC_processLotArray();

                contact.Id = int.Parse(xmlContact.Attribute("Id").Value);
                contact.name = xmlContact.Element("name").Value;
                contact.dataType = xmlContact.Element("dataType").Value;
                contact.trayValue = xmlContact.Element("trayValue").Value;
                return contact;
            }
        }

        public void AddContact(Model_CWC_processLotArray contact)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");

                //将值插入xml文档
                xmlDocument.Element("Models").Add(
                    new XElement("Model",
                        new XAttribute("Id", contact.Id),
                        new XElement("name", contact.name),
                        new XElement("dataType", contact.dataType),
                        new XElement("trayValue", contact.trayValue)
                        ));

                xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");
            }
        }

        public void UpdateContact(Model_CWC_processLotArray contact)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");

                //通过ID更新
                XElement xmlContact = xmlDocument.Element("Models").Elements("Model")
                    .Where(x => x.Attribute("Id").Value == contact.Id.ToString())
                    .FirstOrDefault();

                //更新
                xmlContact.SetElementValue("name", contact.name);
                xmlContact.SetElementValue("dataType", contact.dataType);
                xmlContact.SetElementValue("trayValue", contact.trayValue);
                xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");
            }
        }

        public void DeleteContact(int id)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");

                //得到 并删除他
                xmlDocument.Root.Elements().Where(x => x.Attribute("Id").Value == id.ToString())
                    .FirstOrDefault().Remove();

                xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");
            }
        }

        public int GetNextId()
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                int currentId;
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");

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
                if (!(File.Exists(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml")))
                {
                    XDocument xmlDocument = new XDocument(
                        new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("Models"));

                    xmlDocument.Save(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");
                }
            }
        }

        public bool IdExists(int id)
        {
            lock (GlobalsInfo.Global_Global.thisLock_xml)
            {
                XDocument xmlDocument = XDocument.Load(AddressFile_Mes.dbPath + "\\Model_CWC_processLotArray.xml");
                return xmlDocument.Element("Models").Elements("Model")
                    .Select(x => int.Parse(x.Attribute("Id").Value))
                    .Where(y => y == id).Count() > 0;
            }
        }





    }

}
