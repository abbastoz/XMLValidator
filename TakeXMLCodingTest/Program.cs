using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace TakeXMLCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Abbas Toz");
            Console.WriteLine("Take home coding test (XML)");   
            string example= "<tutorial date='01 / 01 / 2000'>XML</tutorial>";
            string example2 = "<tutorial date>XML</tutorial date>";

            string exampleFirst = "<Design><Code>hello world</Code></Design>";
			string exampleSecond = "<Design><Code>hello world</Code></Design><People>";
			string exampleThird = "<People><Design><Code>hello world</People></Code></Design>";
			string exampleFourth = "<People age=”1”>hello world</People>";

			Console.WriteLine(example + " is a valid XML :" + DetermineXml(example));
			Console.WriteLine(example2 + " is a valid XML :" + DetermineXml(example2));
			Console.WriteLine(exampleFirst+ " is a valid XML :" + DetermineXml(exampleFirst));
			Console.WriteLine(exampleSecond + " is a valid XML :" + DetermineXml(exampleSecond));
			Console.WriteLine(exampleThird+ " is a valid XML :" + DetermineXml(exampleThird));
			Console.WriteLine(exampleFourth + " is a valid XML :" + DetermineXml(exampleFourth));
			Console.ReadKey();
        }
        public static bool DetermineXml(string xml)
        {
            var count = 0; //tag count
            var stack = new Stack<Tag>();            
           
            for (var i = 0; i < xml.Length; i++)
            {
                if (xml[i] == ' ') continue;              

                    if (xml[i] == '<')
                    {
                        var next = xml.IndexOf('>', i + 1);
                        if (next <= 0) return false;

                        var name = xml.Substring(i + 1, next - (i + 1));
                        if (name.Length < 1) return false;
                        if (xml[i + 1] == '/')
                        {
                            name = name.Substring(1);
                            //end tag

                            if (stack.Count == 0) return false;
                            var tag = stack.Pop();
                            if (!tag.TagName.Equals(name)) return false;
                        }
                        else
                        {
                            if (count > 0 && stack.Count == 0) return false;

                            //start tag
                            var tag = new Tag()
                            {
                                TagName = name,
                            };
                            stack.Push(tag);
                            count++;
                        }
                       
                        i = next;
                    }
                    else if (count == 0)
                    {
                        return false;
                    }
                    else if (stack.Count == 0)
                    {
                        return false;
                    }
                }
            
            return stack.Count == 0;
        }


        
        public class Tag
        {
            public string TagName;           
        }
    }
}
