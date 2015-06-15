using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ADMInteractive_Solution
{
    public partial class list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Setting the root path for loading the XML file
            string rootPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string dataPath = rootPath.Replace(@"\bin\debug", "");

            dataPath += "Data/List.xml";

            // Loading the List.xml file
            XDocument dataFile = XDocument.Load(dataPath);

            // Filtering through the Store Element
            XElement store = new XElement(dataFile.Element("Store"));

            // Selecting all the Product Elements under Products Element using XML.LINQ
            var productsList = from product in store.Descendants("Products").Descendants("Product")
                               select new
                               {
                                   id = product.Attribute("id").Value,
                                   title = product.Descendants().ElementAt(0).Value,
                                   description = product.Descendants().ElementAt(1).Value,
                                   image = product.Descendants().ElementAt(2).Value,
                                   popularity = product.Descendants("Sorting").Descendants().ElementAt(0).Value
                               };
            
            // Assigning the products list to the Repeater Control and Binding the Data
            Repeater1.DataSource = productsList.ToList<Object>();
            Repeater1.DataBind();

        }

    }
}