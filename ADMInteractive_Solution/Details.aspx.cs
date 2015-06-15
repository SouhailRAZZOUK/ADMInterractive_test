using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Xml.Linq;


namespace ADMInteractive_Solution
{
    public partial class Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Getting the selected product id
            string productId = Request.QueryString["productId"];

            // Setting the root path for loading the XML file
            string rootPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string dataPath = rootPath.Replace(@"\bin\debug", "");

            // Loading the Details.xml file
            XDocument listFile = XDocument.Load(dataPath + "Data/List.xml");
            XDocument detailsFile = XDocument.Load(dataPath + "Data/Detail.xml");

            // Filtering through the Store Element
            XElement store = new XElement(listFile.Element("Store"));

            // Selecting the corresponding Product Element(s) under Products Element using XML.LINQ according to the selected product from 

            var products = from product in store.Descendants("Products").Descendants("Product")
                           join productDetail in detailsFile.Element("Store").Descendants("Products").Descendants("Product")
                                on (string)product.Attribute("id") equals (string)productDetail.Attribute("id")
                               
                           where product.Attribute("id").Value == productId
                           select new
                           {
                               id = product.Attribute("id").Value,
                               title = product.Element("Title").Value,
                               description = product.Element("Description").Value,
                               image = product.Element("Image").Value,
                               price = product.Element("Price").Value,
                               specs =  from spec in productDetail.Descendants("Specs").Descendants("Spec")
                                        select new
                                        {
                                            specText = spec.Value
                                        }
                           };

            Repeater1.DataSource = products.ToList<Object>();
            Repeater1.DataBind();

            //var repeater2 = (Repeater)Repeater1.FindControl("Repeater2");
            //repeater2.DataSource = products.ToList<Object>();
            //repeater2.DataBind();

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string GetProductsPriceAndAvailablity()
        {
       
            // Setting the root path for loading the XML file
            string rootPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string dataPath = rootPath.Replace(@"\bin\debug", "");

            // Loading XML Files
            XDocument listFile = XDocument.Load(dataPath + "Data/List.xml");
            XDocument detailsFile = XDocument.Load(dataPath + "Data/Detail.xml");

            // Filtering through the Store Element
            XElement store = new XElement(listFile.Element("Store"));

            var productsAvPr = from product in store.Descendants("Products").Descendants("Product")
                               join productDetail in detailsFile.Element("Store").Descendants("Products").Descendants("Product")
                                    on (string)product.Attribute("id") equals (string)productDetail.Attribute("id")
                               select new
                               {
                                   id = product.Attribute("id").Value,
                                   price = product.Element("Price").Value,
                                   availability = productDetail.Element("Availability").Value
                               };

            // Serializing the Products list to JSON Data
            JavaScriptSerializer products = new JavaScriptSerializer();
            string output = products.Serialize(productsAvPr.ToList<Object>());

            // Returning the results
            return output; 

        }
        
    }
}