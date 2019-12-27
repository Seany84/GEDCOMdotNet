using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace GEDCOMdotNet.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var gedcomFileLines = File.ReadAllLines("Resources\\TGC551.ged");

            Assert.True(gedcomFileLines.Length > 0);

            var entities = new List<LineEntity>();

            foreach(var line in gedcomFileLines)
            {
                var x = new LineEntity(line);

                entities.Add(x);
            }
        }
    }

    public class LineEntity
    {
        public LineEntity(string line)
        {
            if(string.IsNullOrEmpty(line))
                throw new Exception("A GEDCOM line cannot be empty.");
            if (line.StartsWith(' '))
                throw new Exception("A GEDCOM line cannot start with a space.");
            if (line.EndsWith(' '))
                throw new Exception("A GEDCOM line cannot end with a space.");

            var lineTokenised = line.Split(' ');
            Id = lineTokenised[0];
            Action = lineTokenised[1];
            Value = lineTokenised.Aggregate((curr, next) => curr + " " + next);
        }

        public string Id { get; }
        public string Action { get; }
        public string Value { get; }
    }

    public class HeaderEntity
    {
        [GedcomAction("SOUR")]
        public string Source { get; set; }
        [GedcomAction("NAME")]
        public string Name { get; set; }
        [GedcomAction("VERS")]
        public string Version { get; set; }
        [GedcomAction("CORP")]
        public string Corporation { get; set; }
        [GedcomAction("ADDR")]
        public string Address { get; set; }
        [GedcomAction("ADDR1")]
        public string Address1 { get; set; }
        [GedcomAction("ADDR2")]
        public string Address2 { get; set; }
        [GedcomAction("CITY")]
        public string City { get; set; }
        [GedcomAction("STAE")]
        public string State { get; set; }
        [GedcomAction("POST")]
        public string Postcode { get; set; }
        [GedcomAction("CTRY")]
        public string Country { get; set; }
        [GedcomAction("PHONE")]
        public List<string> PhoneNumbers { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class GedcomActionAttribute : Attribute
    {
        public GedcomActionAttribute(string name)
        {

        }
    }
}
