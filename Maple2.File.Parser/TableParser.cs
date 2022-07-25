﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Maple2.File.IO;
using Maple2.File.Parser.Tools;
using Maple2.File.Parser.Xml.Table;

namespace Maple2.File.Parser;

public class TableParser {
    private readonly M2dReader xmlReader;
    private readonly XmlSerializer paletteSerializer;
    private readonly XmlSerializer defaultItemsSerializer;
    private readonly XmlSerializer dungeonRoomSerializer;
    private readonly XmlSerializer instrumentCategoryInfoSerializer;
    private readonly XmlSerializer instrumentInfoSerializer;
    private readonly XmlSerializer interactObjectSerializer;
    private readonly XmlSerializer itemBreakIngredientSerializer;
    private readonly XmlSerializer itemGemstoneUpgradeSerializer;
    private readonly XmlSerializer itemLapenshardUpgradeSerializer;
    private readonly XmlSerializer jobSerializer;
    private readonly XmlSerializer magicPathSerializer;
    private readonly XmlSerializer mapSpawnTagSerializer;
    private readonly XmlSerializer petSpawnInfoSerializer;
    private readonly XmlSerializer setItemInfoSerializer;
    private readonly XmlSerializer setItemOptionSerializer;

    public TableParser(M2dReader xmlReader) {
        this.xmlReader = xmlReader;
        this.paletteSerializer = new XmlSerializer(typeof(ColorPaletteRoot));
        this.defaultItemsSerializer = new XmlSerializer(typeof(DefaultItems));
        this.dungeonRoomSerializer = new XmlSerializer(typeof(DungeonRoomRoot));
        this.instrumentCategoryInfoSerializer = new XmlSerializer(typeof(InstrumentCategoryInfoRoot));
        this.instrumentInfoSerializer = new XmlSerializer(typeof(InstrumentInfoRoot));
        this.interactObjectSerializer = new XmlSerializer(typeof(InteractObjectRoot));
        this.itemBreakIngredientSerializer = new XmlSerializer(typeof(ItemBreakIngredientRoot));
        this.itemGemstoneUpgradeSerializer = new XmlSerializer(typeof(ItemGemstoneUpgradeRoot));
        this.itemLapenshardUpgradeSerializer = new XmlSerializer(typeof(ItemLapenshardUpgradeRoot));
        this.jobSerializer = new XmlSerializer(typeof(JobRoot));
        this.magicPathSerializer = new XmlSerializer(typeof(MagicPath));
        this.mapSpawnTagSerializer = new XmlSerializer(typeof(MapSpawnTag));
        this.petSpawnInfoSerializer = new XmlSerializer(typeof(PetSpawnInfoRoot));
        this.setItemInfoSerializer = new XmlSerializer(typeof(SetItemInfoRoot));
        this.setItemOptionSerializer = new XmlSerializer(typeof(SetItemOptionRoot));
    }

    public IEnumerable<(int Id, ColorPalette Palette)> ParseColorPalette() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/colorpalette.xml"));
        var data = paletteSerializer.Deserialize(reader) as ColorPaletteRoot;
        Debug.Assert(data != null);

        foreach (ColorPalette palette in data.color) {
            yield return (palette.id, palette);
        }
    }

    public IEnumerable<(int Id, ColorPalette Palette)> ParseColorPaletteAchieve() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/na/colorpalette_achieve.xml"));
        var data = paletteSerializer.Deserialize(reader) as ColorPaletteRoot;
        Debug.Assert(data != null);

        foreach (ColorPalette palette in data.color) {
            yield return (palette.id, palette);
        }
    }

    public IEnumerable<(int JobCode, string Slot, IList<DefaultItems.Item> Items)> ParseDefaultItems() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/defaultitems.xml"));
        var data = defaultItemsSerializer.Deserialize(reader) as DefaultItems;
        Debug.Assert(data != null);

        foreach (DefaultItems.Key key in data.key) {
            foreach (DefaultItems.Slot slot in key.slot) {
                yield return (key.jobCode, slot.name, slot.item);
            }
        }
    }

    public IEnumerable<(int Id, DungeonRoom Item)> ParseDungeonRoom() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/na/dungeonroom.xml"));
        var data = dungeonRoomSerializer.Deserialize(reader) as DungeonRoomRoot;
        Debug.Assert(data != null);

        foreach (DungeonRoom dungeon in data.dungeonRoom) {
            yield return (dungeon.dungeonRoomID, dungeon);
        }
    }

    public IEnumerable<(int Id, InstrumentCategoryInfo Info)> ParseInstrumentCategoryInfo() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/instrumentcategoryinfo.xml"));
        var data = instrumentCategoryInfoSerializer.Deserialize(reader) as InstrumentCategoryInfoRoot;
        Debug.Assert(data != null);

        foreach (InstrumentCategoryInfo instrument in data.category) {
            yield return (instrument.id, instrument);
        }
    }

    public IEnumerable<(int Id, InstrumentInfo Info)> ParseInstrumentInfo() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/instrumentinfo.xml")));
        xml = Sanitizer.SanitizeBool(xml);
        var reader = XmlReader.Create(new StringReader(xml));
        var data = instrumentInfoSerializer.Deserialize(reader) as InstrumentInfoRoot;
        Debug.Assert(data != null);

        foreach (InstrumentInfo instrument in data.instrument) {
            yield return (instrument.id, instrument);
        }
    }

    public IEnumerable<(int Id, InteractObject Info)> ParseInteractObject() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/interactobject.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = interactObjectSerializer.Deserialize(reader) as InteractObjectRoot;
        Debug.Assert(data != null);

        foreach (InteractObject interact in data.interact) {
            yield return (interact.id, interact);
        }
    }

    public IEnumerable<(int Id, InteractObject Info)> ParseInteractObjectMastery() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/interactobject_mastery.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = interactObjectSerializer.Deserialize(reader) as InteractObjectRoot;
        Debug.Assert(data != null);

        foreach (InteractObject interact in data.interact) {
            yield return (interact.id, interact);
        }
    }

    public IEnumerable<(int ItemId, ItemBreakIngredient Item)> ParseItemBreakIngredient() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/itembreakingredient.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemBreakIngredientSerializer.Deserialize(reader) as ItemBreakIngredientRoot;
        Debug.Assert(data != null);

        foreach (ItemBreakIngredient item in data.item) {
            yield return (item.ItemID, item);
        }
    }

    public IEnumerable<(int ItemId, ItemGemstoneUpgrade Item)> ParseItemGemstoneUpgrade() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/na/itemgemstoneupgrade.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemGemstoneUpgradeSerializer.Deserialize(reader) as ItemGemstoneUpgradeRoot;
        Debug.Assert(data != null);

        foreach (ItemGemstoneUpgrade key in data.key) {
            yield return (key.ItemId, key);
        }
    }

    public IEnumerable<(int ItemId, ItemLapenshardUpgrade Item)> ParseItemLapenshardUpgrade() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/na/itemlapenshardupgrade.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = itemLapenshardUpgradeSerializer.Deserialize(reader) as ItemLapenshardUpgradeRoot;
        Debug.Assert(data != null);

        foreach (ItemLapenshardUpgrade key in data.key) {
            yield return (key.ItemId, key);
        }
    }

    public IEnumerable<JobTable> ParseJobTable() {
        string xml = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/job.xml")));
        var reader = XmlReader.Create(new StringReader(xml));
        var data = jobSerializer.Deserialize(reader) as JobRoot;
        Debug.Assert(data != null);

        foreach (JobTable job in data.job) {
            yield return job;
        }
    }

    public IEnumerable<(long Id, MagicType Type)> ParseMagicPath() {
        string sanitized = Sanitizer.SanitizeMagicPath(xmlReader.GetString(xmlReader.GetEntry("table/magicpath.xml")));
        var data = magicPathSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as MagicPath;
        Debug.Assert(data != null);

        foreach (MagicType type in data.type) {
            yield return (type.id, type);
        }
    }

    public IEnumerable<(int MapId, IEnumerable<MapSpawnTag.Region> Region)> ParseMapSpawnTag() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/mapspawntag.xml")));
        var data = mapSpawnTagSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as MapSpawnTag;
        Debug.Assert(data != null);

        foreach (IGrouping<int, MapSpawnTag.Region> group in data.region.GroupBy(region => region.mapCode)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(int FieldId, IEnumerable<PetSpawnInfo> Info)> ParsePetSpawnInfo() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/petspawninfo.xml"));
        var data = petSpawnInfoSerializer.Deserialize(reader) as PetSpawnInfoRoot;
        Debug.Assert(data != null);

        foreach (IGrouping<int, PetSpawnInfo> group in data.SpawnInfo.GroupBy(spawnInfo => spawnInfo.fieldID)) {
            yield return (group.Key, group);
        }
    }

    public IEnumerable<(int Id, SetItemInfo Info)> ParseSetItemInfo() {
        string sanitized = Sanitizer.RemoveEmpty(xmlReader.GetString(xmlReader.GetEntry("table/setiteminfo.xml")));
        var data = setItemInfoSerializer.Deserialize(XmlReader.Create(new StringReader(sanitized))) as SetItemInfoRoot;
        Debug.Assert(data != null);

        foreach (SetItemInfo info in data.set) {
            yield return (info.id, info);
        }
    }

    public IEnumerable<(int Id, SetItemOption Option)> ParseSetItemOption() {
        XmlReader reader = xmlReader.GetXmlReader(xmlReader.GetEntry("table/setitemoption.xml"));
        var data = setItemOptionSerializer.Deserialize(reader) as SetItemOptionRoot;
        Debug.Assert(data != null);

        foreach (SetItemOption option in data.option) {
            yield return (option.id, option);
        }
    }
}