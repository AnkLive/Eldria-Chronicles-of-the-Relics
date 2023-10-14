using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using ItemSystem;
using UnityEngine;

public class ItemConverter : JsonConverter<Item>
{
    public override Item ReadJson(JsonReader reader, Type objectType, Item existingValue, bool hasExistingValue, JsonSerializer serializer)
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

    public override void WriteJson(JsonWriter writer, Item value, JsonSerializer serializer)
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
                WriteArtefactProperties(jsonObject, (ArtefactItem)value);
                break;
            case EItemType.Weapon:
                WriteWeaponProperties(jsonObject, (WeaponItem)value);
                break;
            case EItemType.Spell:
                WriteSpellProperties(jsonObject, (SpellItem)value);
                break;
            // Добавьте другие типы Item по мере необходимости
        }

        jsonObject.WriteTo(writer);
    }

    private ArtefactItem CreateArtefactItem(string itemId, JObject jsonObject)
    {
        // Создать и вернуть ArtefactItem на основе данных из JSON
        ArtefactItem artefactItem = ScriptableObject.CreateInstance<ArtefactItem>();
        artefactItem.ItemId = itemId;
        artefactItem.ItemType = jsonObject["ItemType"].ToObject<EItemType>();
        artefactItem.IsEquipment = jsonObject["IsEquipment"].ToObject<bool>();
        artefactItem.Strength = jsonObject["Strength"].ToObject<float>();
        artefactItem.BoostDamage = jsonObject["BoostDamage"].ToObject<int>();
        artefactItem.BoostHealth = jsonObject["BoostHealth"].ToObject<int>();
        artefactItem.BoostMovementSpeed = jsonObject["BoostMovementSpeed"].ToObject<int>();
        return artefactItem;
    }

    private WeaponItem CreateWeaponItem(string itemId, JObject jsonObject)
    {
        // Создать и вернуть WeaponItem на основе данных из JSON
        WeaponItem weaponItem = ScriptableObject.CreateInstance<WeaponItem>();
        weaponItem.ItemId = itemId;
        weaponItem.ItemType = jsonObject["ItemType"].ToObject<EItemType>();
        weaponItem.IsEquipment = jsonObject["IsEquipment"].ToObject<bool>();
        weaponItem.Damage = jsonObject["Damage"].ToObject<int>();
        return weaponItem;
    }

    private SpellItem CreateSpellItem(string itemId, JObject jsonObject)
    {
        // Создать и вернуть SpellItem на основе данных из JSON
        SpellItem spellItem = ScriptableObject.CreateInstance<SpellItem>();
        spellItem.ItemId = itemId;
        spellItem.ItemType = jsonObject["ItemType"].ToObject<EItemType>();
        spellItem.IsEquipment = jsonObject["IsEquipment"].ToObject<bool>();
        spellItem.Strength = jsonObject["Strength"].ToObject<float>();
        spellItem.Damage = jsonObject["Damage"].ToObject<int>();
        spellItem.Cooldown = jsonObject["Cooldown"].ToObject<int>();
        return spellItem;
    }

    private void WriteArtefactProperties(JObject jsonObject, ArtefactItem artefactItem)
    {
        jsonObject["Strength"] = artefactItem.Strength;
        jsonObject["BoostDamage"] = artefactItem.BoostDamage;
        jsonObject["BoostHealth"] = artefactItem.BoostHealth;
        jsonObject["BoostMovementSpeed"] = artefactItem.BoostMovementSpeed;
        // Добавьте другие свойства ArtefactItem, если они есть
    }

    private void WriteWeaponProperties(JObject jsonObject, WeaponItem weaponItem)
    {
        jsonObject["Damage"] = weaponItem.Damage;
        // Добавьте другие свойства WeaponItem, если они есть
    }

    private void WriteSpellProperties(JObject jsonObject, SpellItem spellItem)
    {
        jsonObject["Strength"] = spellItem.Strength;
        jsonObject["Damage"] = spellItem.Damage;
        jsonObject["Cooldown"] = spellItem.Cooldown;
        // Добавьте другие свойства SpellItem, если они есть
    }
}