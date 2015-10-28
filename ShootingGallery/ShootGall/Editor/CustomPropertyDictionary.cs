using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
    
namespace Editor
{
    //This code is from http://stackoverflow.com/a/3491982 with major changes
    // by Kevin Forest.
    //This converter is needed so that our class can be treated as an expandable 
    // property (i.e. a '+' sign appears next to the field and allows it to be 
    // expanded in the property grid.
    [TypeConverter(typeof(CustomPropertyDictionary.CustomPropertyDictionaryConverter))]
    public class CustomPropertyDictionary
    {
        //The dictionaries name.
        [Browsable(false)]
        public string Name { get; set; }
        //List of the properties with an exposed public interface.
        private readonly List<CustomProperty> props = new List<CustomProperty>();
        [Browsable(false)]
        private List<CustomProperty> Properties { get { return props; } }

        //The dictionary of properties that this class wraps.
        private Dictionary<string, object> values = new Dictionary<string, object>();

        //Public accessor for getting and setting values from the dictionary.
        //If you wish to create a new property, use TryAdd().
        public object this[string name]
        {
            get
            {
                //Attempt to get the value from the dictionary. If it is
                // currently equal to null use the default value.
                object val;
                if (values.TryGetValue(name, out val))
                {
                    //Ug, this is so inefficient. Need to find a better way to do this.
                    if (val == null)
                    {
                        foreach (CustomProperty prop in Properties)
                        {
                            if (prop.Name == name)
                            {
                                val = prop.DefaultValue;
                            }
                        }
                    }
                }
                return val;
            }
            //Does nothing if dictionary does not contain this key.
            set
            {
                
                if (values.ContainsKey(name))
                {
                    values[name] = value;
                }
            } //Changed by KF to allow mutable properties and to ignore attempts to create a value.
        }

        //Try and delete a value. Returns false (and does nothing) if the key is not found, true otherwise.
        // Removes the property from both the dictionary and the list.
        public bool TryDelete(String name)
        {
            if (!values.ContainsKey(name))
            {
                return false;
            }
            foreach (CustomProperty prop in Properties)
            {
                if (prop.Name == name)
                {
                    Properties.Remove(prop);
                    break;
                }
            }
            values.Remove(name);
            return true;
        }

        //Try and add a new property. Returns false (and does nothing) if the key is already in the dictionary, 
        // otherwise returns true and adds the property. It sets the value in the dictionary to null, indicating
        // the default value should be used for now.
        public bool TryAdd(CustomProperty newCp)
        {
            if (values.ContainsKey(newCp.Name))
            {
                return false;
            }
            Properties.Add(newCp);
            values[newCp.Name] = null;
            return true;
        }

        //Ho boy. This is a tentative start at adding code that allows you to rename a property.
        // Would not rely on it just yet to be completely stable.
        //Intent is to return false if the key is not in the dictionary or if it cannot rename the key because
        // the new name is already taken.
        public bool TryRename(string oldName, string newName)
        {
            if (values.ContainsKey(oldName) && !values.ContainsKey(newName))
            {
                object value = values[oldName];
                values.Remove(oldName);
                values[newName] = value;
                foreach (CustomProperty prop in Properties)
                {
                    if (prop.Name == oldName)
                    {
                        prop.Name = newName;
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        //Exports each property as an XMLElement with the values assigned to that
        // property stored in the XMLs inner text. Also stores the type of the 
        // property as an attribute of each element, allowing for dynamic loading.
        //Includes some special code for outputting rectangles and colors as
        // single spaced sequence of values instead of the default C# serialisation.
        public void ToXml(XmlElement parent, XmlDocument doc)
        {
            foreach (CustomProperty cp in Properties)
            {
                object value = values[cp.Name];
                if (value == null)
                {
                    value = cp.DefaultValue;
                }
                XmlElement el = doc.CreateElement(cp.Name);
                XmlAttribute tatt = doc.CreateAttribute("TypeStr");
                tatt.Value = cp.Type.ToString();
                el.Attributes.Append(tatt);
                if (cp.Type == typeof(Color))
                {
                    Color c = (Color)value;
                    el.InnerText = c.R + " " + c.G + " " + c.B + " " + c.A;
                }
                else if (cp.Type == typeof(Rectangle))
                {
                    Rectangle r = (Rectangle)value;
                    el.InnerText = r.X + " " + r.Y + " " + r.Width + " " + r.Height;
                }
                else
                {
                    el.InnerText = value.ToString();
                }
                parent.AppendChild(el);
            }
        }

        //Coverter class - allows a class to be treated as another type without requiring it to 
        // inherit from it. Useful for when you want the functionality of inheritance without the
        // baggage.
        private class CustomPropertyDictionaryConverter : ExpandableObjectConverter
        {
            //Get all the properties in the dictionary and place them into a PropertyDescriptorCollection.
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                //This gets the properties that are a part of the base class.
                var stdProps = base.GetProperties(context, value, attributes);
                //In this case value is the object being type converted, which is our custom property dictionary.
                CustomPropertyDictionary obj = value as CustomPropertyDictionary;
                //Null check. Better safe than sorry!
                List<CustomProperty> customProps = obj == null ? null : obj.Properties;
                //Create a property descriptor array that is sufficiently large to hold all of the standard properties
                // and the custom properties from our dictionary.
                PropertyDescriptor[] props = new PropertyDescriptor[stdProps.Count + (customProps == null ? 0 : customProps.Count)];
                //Copy the standard properties to the array.
                stdProps.CopyTo(props, 0);
                if (customProps != null)
                {
                    //Iterate through the custom properties and add them to the array, starting
                    // from the first index past where we copied in the standard properties.
                    int index = stdProps.Count;
                    foreach (CustomProperty prop in customProps)
                    {
                        props[index++] = new CustomPropertyDescriptor(prop);
                    }
                }
                return new PropertyDescriptorCollection(props);
            }
        }

        //A descriptor for the custom properties in our dictionary. Allows us to set
        // things like the name, description, category, and values.
        private class CustomPropertyDescriptor : PropertyDescriptor
        {
            private readonly CustomProperty prop;
            public CustomPropertyDescriptor(CustomProperty prop)
                : base(prop.Name, null)
            {
                this.prop = prop;
            }
            public override string Category { get { return "Entity Properties"; } }
            public override string Description { get { return prop.Desc + " " + prop.Type.ToString(); } }
            public override string Name { get { return prop.Name; } }
            public override bool ShouldSerializeValue(object component) { return ((CustomPropertyDictionary)component)[prop.Name] != null; }
            public override void ResetValue(object component) { ((CustomPropertyDictionary)component)[prop.Name] = null; }
            public override bool IsReadOnly { get { return false; } }
            public override Type PropertyType { get { return prop.Type; } }
            public override bool CanResetValue(object component) { return true; }
            public override Type ComponentType { get { return typeof(CustomPropertyDictionary); } }
            public override void SetValue(object component, object value) { ((CustomPropertyDictionary)component)[prop.Name] = value; }
            public override object GetValue(object component) { return ((CustomPropertyDictionary)component)[prop.Name] ?? prop.DefaultValue; }
        }
    }

    //A custom property object (this is what the dictionary stores). Can have any legal type, although
    // for it to be useful it needs to export to a format that can fit into an XML file and must be 
    // importable on the other side.
    public class CustomProperty
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public object DefaultValue { get; set; }
        Type type;

        public Type Type
        {
            get
            {
                return type;
            }
            //This code checks the type and attempts to create a
            // sensible default value.
            set
            {
                type = value;
                //Value types are always constructable.
                if (type.IsValueType)
                {
                    DefaultValue = Activator.CreateInstance(value);
                //Special handler for strings, which are immutable reference types and
                // thus a royal pain.
                } else if(type == typeof(string)) {
                    DefaultValue = "";
                //Otherwise see if there is a default constructor available for this type.
                // If there is, call it, otherwise use null.
                } else {
                    var constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor != null)
                    {
                        DefaultValue = constructor.Invoke(null);
                    }
                    else
                    {
                        DefaultValue = null;
                    }
                }
                
            }
        }
    }
}
