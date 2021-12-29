﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Maple2.File.Parser.Tools;
using Maple2.File.Parser.Xml.AdditionalEffect;
using Maple2.File.Parser.Xml.Skill;

namespace Maple2.File.Parser.Xml;

// ./data/xml/additionaleffect/%08d.xml
[XmlRoot("ms2")]
public class AdditionalEffectLevelData {
    [XmlElement] public List<AdditionalEffectData> level;

    internal List<AdditionalEffectData> Filter(Filter filter) {
        return level
            .Where(data => filter.FeatureEnabled(data.feature) && filter.HasLocale(data.locale))
            .ToList();
    }
}

public class AdditionalEffectData {
    [XmlAttribute] public string locale = string.Empty;
    [XmlAttribute] public string feature = string.Empty;

    [XmlElement] public BeginCondition beginCondition;
    [XmlElement] public BasicProperty BasicProperty;
    [XmlElement] public MotionProperty MotionProperty;
    [XmlElement] public CancelEffectProperty CancelEffectProperty;
    [XmlElement] public ImmuneEffectProperty ImmuneEffectProperty;
    [XmlElement] public ResetSkillCoolDownTimeProperty ResetSkillCoolDownTimeProperty;
    [XmlElement] public ModifyEffectDurationProperty ModifyEffectDurationProperty;
    [XmlElement] public ModifyOverlapCountProperty ModifyOverlapCountProperty;
    [XmlElement] public StatusProperty StatusProperty;
    [XmlElement] public FinalStatusProperty FinalStatusProperty;
    [XmlElement] public OffensiveProperty OffensiveProperty;
    [XmlElement] public DefensiveProperty DefensiveProperty;
    [XmlElement] public RecoveryProperty RecoveryProperty;
    [XmlElement] public ExpProperty ExpProperty;
    [XmlElement] public DotDamageProperty DotDamageProperty;
    [XmlElement] public DotBuffProperty DotBuffProperty;
    [XmlElement] public ConsumeProperty ConsumeProperty;
    [XmlElement] public ReflectProperty ReflectProperty;
    [XmlElement] public UIProperty UIProperty;
    [XmlElement] public ShieldProperty ShieldProperty;
    [XmlElement] public MesoGuardProperty MesoGuardProperty;
    [XmlElement] public InvokeEffectProperty InvokeEffectProperty;
    [XmlElement] public SpecialEffectProperty SpecialEffectProperty;
    [XmlElement] public RideeProperty RideeProperty;

    [XmlElement] public List<TriggerSkill> splashSkill;
    [XmlElement] public List<TriggerSkill> conditionSkill;
}