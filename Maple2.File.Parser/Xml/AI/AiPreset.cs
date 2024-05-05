﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Maple2.File.Parser.Xml.AI;

public class AiPreset {
    [XmlAttribute] public string name;

    [XmlElement] public List<Node> node = new List<Node>();
    [XmlElement] public List<AiPreset> aiPreset = new List<AiPreset>();
}