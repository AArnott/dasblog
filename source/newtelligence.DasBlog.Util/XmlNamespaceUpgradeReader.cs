using System;
using System.IO;
using System.Xml;

namespace newtelligence.DasBlog.Util
{
    //SDH: Requires FullTrust
    //public class XmlNamespaceUpgradeReader : XmlTextReader
    //{
    //    string oldNamespaceUri;
    //    string newNamespaceUri;

    //    public XmlNamespaceUpgradeReader( TextReader reader, string oldNamespaceUri, string newNamespaceURI ):base( reader )
    //    {
    //        this.oldNamespaceUri = oldNamespaceUri;
    //        this.newNamespaceUri = newNamespaceURI;
    //    }

    //    public override string NamespaceURI
    //    {
    //        get
    //        {
    //            // we are assuming XmlSchemaForm.Unqualified, therefore
    //            // we can't switch the NS here
    //            if ( this.NodeType != XmlNodeType.Attribute && 
    //                 base.NamespaceURI == oldNamespaceUri )
    //            {
    //                return newNamespaceUri;
    //            }
    //            else 
    //            {
    //                return base.NamespaceURI;
    //            }
    //        }
    //    }

    //}
}
