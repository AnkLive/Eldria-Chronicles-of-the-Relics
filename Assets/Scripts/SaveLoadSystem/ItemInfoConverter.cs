using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class ItemConverter : JsonConverter<ItemBase>
{
    public override ItemBase ReadJson(JsonReader reader, Type objectType, ItemBase existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        EItemType itemType = jsonObject["ItemType"]!.ToObject<EItemType>();
        string itemId = jsonObject["ItemId"]?.ToString();

        switch (itemType)
        {
            case EItemType.Artefact:
                return CreateArtefactItem(itemId, jsonObject);
            case EItemType.Weapon:
                return CreateWeaponItem(itemId, jsonObject);
            case EItemType.Spell:
                return CreateSpellItem(itemId, jsonObject);
            default:
                return null;
        }
    }

    public override void WriteJson(JsonWriter writer, ItemBase value, JsonSerializer serializer)
    {
        JObject jsonObject = new JObject();
        jsonObject["ItemType"] = value.ItemType.ToString();
        jsonObject["ItemId"] = value.ItemId;
        jsonObject["IsEquipment"] = value.IsEquipment;
        
        
        if (!string.IsNullOrEmpty(value.name))
        {
            jsonObject["name"] = value.name;
        }
        if (!string.IsNullOrEmpty(value.hideFlags.ToString()))
        {
            jsonObject["hideFlags"] = value.hideFlags.ToString();
        }

        // В зависимости от типа Item, добавьте дополнительные свойства
        switch (value.ItemType)
        {
            case EItemType.Artefact:
                WriteArtefactProperties(jsonObject, (ArtefactItemBase)value);
                break;
            case EItemType.Weapon:
                WriteWeaponProperties(jsonObject, (WeaponItemBase)value);
                break;
            case EItemType.Spell:
                WriteSpellProperties(jsonObject, (SpellItemBase)value);
                break;
            // Добавьте другие типы Item по мере необходимости
        }

        jsonObject.WriteTo(writer);
    }

    private ArtefactItemBase CreateArtefactItem(string itemId, JObject jsonObject)
    {
        // Создать и вернуть ArtefactItem на основе данных из JSON
        ArtefactItemBase artefactItemBase = ScriptableObject.CreateInstance<ArtefactItemBase>();
        artefactItemBase.ItemId = itemId;
        artefactItemBase.ItemType = jsonObject["ItemType"].ToObject<EItemType>();
        artefactItemBase.IsEquipment = jsonObject["IsEquipment"].ToObject<bool>();
        artefactItemBase.Strength = jsonObject["Strength"].ToObject<float>();
        artefactItemBase.BoostDamage = jsonObject["BoostDamage"].ToObject<int>();
        artefactItemBase.BoostHealth = jsonObject["BoostHealth"].ToObject<int>();
        artefactItemBase.BoostMovementSpeed = jsonObject["BoostMovementSpeed"].ToObject<int>();
        return artefactItemBase;
    }

    private WeaponItemBase CreateWeaponItem(string itemId, JObject jsonObject)
    {
        // Создать и вернуть WeaponItem на основе данных из JSON
        WeaponItemBase weaponItemBase = ScriptableObject.CreateInstance<WeaponItemBase>();
        weaponItemBase.ItemId = itemId;
        weaponItemBase.ItemType = jsonObject["ItemType"].ToObject<EItemType>();
        weaponItemBase.IsEquipment = jsonObject["IsEquipment"].ToObject<bool>();
        weaponItemBase.StatusType = jsonObject["StatusType"].ToObject<EStatusType>();
        weaponItemBase.AttackDamage = jsonObject["AttackDamage"].ToObject<float>();
        weaponItemBase.FireDamageMultiplier = jsonObject["FireDamageMultiplier"].ToObject<float>();
        weaponItemBase.IceDamageMultiplier = jsonObject["IceDamageMultiplier"].ToObject<float>();
        weaponItemBase.PoisonDamageMultiplier = jsonObject["PoisonDamageMultiplier"].ToObject<float>();
        weaponItemBase.AttackDamageMultiplier = jsonObject["AttackDamageMultiplier"].ToObject<float>();
        weaponItemBase.CriticalChance = jsonObject["CriticalChance"].ToObject<float>();
        weaponItemBase.ElementalChance = jsonObject["ElementalChance"].ToObject<float>();
        weaponItemBase.AttackSpeed = jsonObject["AttackSpeed"].ToObject<float>();
        return weaponItemBase;
    }

    private SpellItemBase CreateSpellItem(string itemId, JObject jsonObject)
    {
        // Создать и вернуть SpellItem на основе данных из JSON
        SpellItemBase spellItemBase = ScriptableObject.CreateInstance<SpellItemBase>();
        spellItemBase.ItemId = itemId;
        spellItemBase.ItemType = jsonObject["ItemType"].ToObject<EItemType>();
        spellItemBase.IsEquipment = jsonObject["IsEquipment"].ToObject<bool>();
        spellItemBase.Strength = jsonObject["Strength"].ToObject<float>();
        spellItemBase.Damage = jsonObject["Damage"].ToObject<int>();
        spellItemBase.Cooldown = jsonObject["Cooldown"].ToObject<int>();
        return spellItemBase;
    }

    private void WriteArtefactProperties(JObject jsonObject, ArtefactItemBase artefactItemBase)
    {
        jsonObject["Strength"] = artefactItemBase.Strength;
        jsonObject["BoostDamage"] = artefactItemBase.BoostDamage;
        jsonObject["BoostHealth"] = artefactItemBase.BoostHealth;
        jsonObject["BoostMovementSpeed"] = artefactItemBase.BoostMovementSpeed;
        // Добавьте другие свойства ArtefactItem, если они есть
    }

    private void WriteWeaponProperties(JObject jsonObject, WeaponItemBase weaponItemBase)
    {
        jsonObject["StatusType"] = weaponItemBase.StatusType.ToString();
        jsonObject["AttackDamage"] = weaponItemBase.AttackDamage;
        jsonObject["FireDamageMultiplier"] = weaponItemBase.FireDamageMultiplier;
        jsonObject["IceDamageMultiplier"] = weaponItemBase.IceDamageMultiplier;
        jsonObject["PoisonDamageMultiplier"] = weaponItemBase.PoisonDamageMultiplier;
        jsonObject["AttackDamageMultiplier"] = weaponItemBase.AttackDamageMultiplier;
        jsonObject["CriticalChance"] = weaponItemBase.CriticalChance;
        jsonObject["ElementalChance"] = weaponItemBase.ElementalChance;
        jsonObject["AttackSpeed"] = weaponItemBase.AttackSpeed;
        // Добавьте другие свойства WeaponItem, если они есть
    }

    private void WriteSpellProperties(JObject jsonObject, SpellItemBase spellItemBase)
    {
        jsonObject["Strength"] = spellItemBase.Strength;
        jsonObject["Damage"] = spellItemBase.Damage;
        jsonObject["Cooldown"] = spellItemBase.Cooldown;
        // Добавьте другие свойства SpellItem, если они есть
    }
}