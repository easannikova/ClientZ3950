using ConsoleApp4.Context;
using ConsoleApp4.Entity;
using ConsoleApp4.Extension;
using ConsoleApp4.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ConsoleApp4.Logic
{
    public class Reader : IDisposable
    {
        private readonly XmlReader _xmlReader;

        public Reader(string filename)
        {
            var xmlSettings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            _xmlReader = XmlReader.Create(filename, xmlSettings);
        }

        public void FillGroup()
        {
            using(var context = new BiblioContext())
            {
                var list = new List<Category>();
                while (_xmlReader.ReadToFollowing(Constant.KeyCategory))
                {
                    var id = _xmlReader.GetAttribute("id");
                    var parentId = _xmlReader.GetAttribute("parentId");
                    var value = _xmlReader.ReadElementContentAsString();
                    var category = new Category
                    {
                        CatId = int.Parse(id),
                        Parent = parentId == null ? null : 
                            list
                            .FirstOrDefault(x => x.Id.ToString() == parentId),
                        Name = value
                    };
                    list.Add(category);
                }
                context.Categories.AddRange(list);
                context.SaveChanges();
            }
        }

        public void FillOffer()
        {
            using (var context = new BiblioContext())
            {
                var list = new List<Offer>();
                while (_xmlReader.ReadToFollowing(Constant.KeyField))
                {
                    var id = _xmlReader.GetAttribute("id");
                    var rTree = _xmlReader.ReadSubtree();
                    rTree.MoveToContent();
                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(rTree.ReadOuterXml());
                    var nodes = xmlDoc.SelectNodes(Constant.KeyField);
                    foreach (XmlNode item in nodes)
                    {
                        var categoryId = item.SelectSingleNode(Field.CategoryId.ToQueryString())?.InnerText;
                        Category category = null;
                        if (categoryId != null)
                            category = context.Categories.FirstOrDefault(x=>x.CatId.ToString() == categoryId);

                        var offer = new Offer
                        {
                            Author = item.SelectSingleNode(Field.Author.ToQueryString())?.InnerText,
                            Description = item.SelectSingleNode(Field.Description.ToQueryString())?.InnerText,
                            Title = item.SelectSingleNode(Field.Name.ToQueryString())?.InnerText,
                            Year = item.SelectSingleNode(Field.Year.ToQueryString())?.InnerText,
                            Publisher = item.SelectSingleNode(Field.Publisher.ToQueryString())?.InnerText,
                            ISBN = item.SelectSingleNode(Field.ISBN.ToQueryString())?.InnerText,
                            Pages = item.SelectSingleNode(Field.Page_Extent.ToQueryString())?.InnerText,
                            OfferId = int.Parse(id),
                            Category = category
                        };

                        list.Add(offer);
                        if (list.Count > 10000)
                        {
                            context.Offers.AddRange(list);
                            context.SaveChanges();
                            list.Clear();
                        }
                    }
                }
                context.Offers.AddRange(list);
                context.SaveChanges();
            }
        }

        public void Execute()
        {
            FillGroup();
            FillOffer();
        }

        public void Dispose()
        {
            _xmlReader?.Dispose();
        }
    }
}
