using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BAO.Clases
{
    public class FileManager
    {
        enum LoadType { Attributes, Contents };

        LoadType type;
        List<List<string>> attributes = new List<List<string>>();
        List<List<string>> contents = new List<List<string>>();
        List<string> tempAttribute;
        List<string> tempContents;
        bool identifierFound = false;
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents)
        {
            using (StreamReader reader = new StreamReader(filename))
            {   //leer línea en el textfile
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains("Load="))
                    {
                        tempAttribute = new List<string>();
                        line.Remove(0, line.IndexOf("=") + 1);
                        type = LoadType.Attributes;
                    }
                    else
                    {
                        tempContents = new List<string>();
                        type = LoadType.Contents;
                    }
                    string[] lineArray = line.Split(']'); //separación por cada ]
                    foreach (string li in lineArray)
                    {
                        string newLine = li.Trim('[', ' ', ']');
                        if (newLine != String.Empty)
                        {
                            if (type == LoadType.Contents)
                            {
                                tempContents.Add(newLine);
                            }
                            else
                            {
                                tempAttribute.Add(newLine);
                            }
                        }
                    }
                    if (type == LoadType.Contents && tempContents.Count > 0)
                    {
                        contents.Add(tempContents);
                        attributes.Add(tempContents);
                    }
                }
            }
        }
        public void LoadContent(string filename, List<List<string>> attributes, List<List<string>> contents, string identifier) //loading n unloading
        {
            using (StreamReader reader = new StreamReader(filename))
            {   //leer línea en el textfile
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.Contains("EndLoad=") && line.Contains(identifier))
                    {
                        identifierFound = false;
                        break;
                    }
                    else
                    {
                        if (line.Contains("Load=") && line.Contains(identifier))
                        {
                            identifierFound = true;
                            continue;
                        }
                    }

                    if (identifierFound)
                    {
                        if (line.Contains("Load="))
                        {
                            tempAttribute = new List<string>();
                            line.Remove(0, line.IndexOf("=") + 1);
                            type = LoadType.Attributes;
                        }
                        else
                        {
                            tempContents = new List<string>();
                            type = LoadType.Contents;
                        }
                        string[] lineArray = line.Split(']'); //separación por cada ]
                        foreach (string li in lineArray)
                        {
                            string newLine = li.Trim('[', ' ', ']');
                            if (newLine != String.Empty)
                            {
                                if (type == LoadType.Contents)
                                {
                                    tempContents.Add(newLine);
                                }
                                else
                                {
                                    tempAttribute.Add(newLine);
                                }
                            }
                        }
                        if (type == LoadType.Contents && tempContents.Count > 0)
                        {
                            contents.Add(tempContents);
                            attributes.Add(tempContents);
                        }
                    }
                }
            }
        }
    }
}
