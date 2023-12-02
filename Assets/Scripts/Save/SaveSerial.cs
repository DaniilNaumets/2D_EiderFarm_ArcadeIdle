using Building;
using Economy;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TriggerScripts;
using UnityEngine;

[RequireComponent(typeof(Menu))]
public class SaveSerial : MonoBehaviour
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private GameObject[] _gagaHouses;
    [SerializeField] private GameObject[] _cleaners;
    [SerializeField] private GameObject[] _clothMachines;
    [SerializeField] private Menu _menu;

    private List<ItemBunch> _items;
    private string path = "/dataSaveFile.dat";

    public SaveData data = new();

    private void Start()
    {
        GetItems();

        if (_menu.GetResetValue())
            ResetData();
        else
            LoadGame();
    }

    private void GetItems()
    {
        _items = _playerInventory.GetAllItems();

    }

    private void Get()
    {
        for (int i = 0; i < _items.Count; i++)
        {

            switch (_items[i].GetItemName())
            {
                case "�������": data.Money = _items[i].GetCount(); break;
                case "������������ ���": data.CleanedFluff = _items[i].GetCount(); break;
                case "�������������� ���": data.UncleanedFluff = _items[i].GetCount(); break;
                case "������": data.Cloth = _items[i].GetCount(); break;
                case "������": data.Cloth = _items[i].GetCount(); break;
            }
        }
    }

    private void Put()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            switch (_items[i].GetItemName())
            {
                case "�������": _playerInventory.GetAllItems()[i].AddItems(data.Money); ; break;
                case "������������ ���": _playerInventory.GetAllItems()[i].AddItems(data.CleanedFluff); break;
                case "�������������� ���": _playerInventory.GetAllItems()[i].AddItems(data.UncleanedFluff); break;
                case "������": _playerInventory.GetAllItems()[i].AddItems(data.Cloth); break;
                case "������": _playerInventory.GetAllItems()[i].AddItems(data.Flag); break;
            }
        }
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        file = !File.Exists(Application.persistentDataPath + path) ?
                File.Create(Application.persistentDataPath + path) :
                File.Open(Application.persistentDataPath + path, FileMode.Open);
        GetItems();
        Get();

        data.GagaHouses = SaveDataGrades(_gagaHouses);
        data.Cleaners = SaveDataGrades(_cleaners);
        data.ClothMachines = SaveDataGrades(_clothMachines);

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(Application.persistentDataPath + path)) return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);

        data = (SaveData)bf.Deserialize(file);
        file.Close();

        ClearAndAdd();
        BuildAndUpgrade(data.GagaHouses, _gagaHouses);
        BuildAndUpgrade(data.Cleaners, _cleaners);
        BuildAndUpgrade(data.ClothMachines, _clothMachines);
    }

    public void ResetData()
    {
        if (!File.Exists(Application.persistentDataPath + path)) return;

        File.Delete(Application.persistentDataPath
                    + path);
        data.Money = 0;
        data.CleanedFluff = 0;
        data.UncleanedFluff = 0;
        data.Cloth = 0;
        data.Flag = 0;

        data.GagaHouses = new int[0];
        data.Cleaners = new int[0];
        data.ClothMachines = new int[0];
    }

    private int[] SaveDataGrades(GameObject[] menus)
    {
        int[] dataArray = new int[menus.Length];

        for (int i = 0; i < menus.Length; i++)
        {
            BuildMenu buildMenu = menus[i].GetComponent<BuildTrigger>().GetBuildMenu();
            if (buildMenu.GetConstruction() != null)
            {
                dataArray[i] = buildMenu.GetConstruction().GetCurrentGrade();
            }
        }

        return dataArray;
    }

    private void ClearAndAdd()
    {
        for (int i = 0; i < _playerInventory.GetAllItems().Count; i++)
        {
            _playerInventory.GetAllItems()[i].ClearItems();
        }
        Put();
    }

    private void BuildAndUpgrade(int[] dataArray, GameObject[] menus)
    {
        for (int i = 0; i < dataArray.Length; i++)
        {
            BuildMenu buildMenu = menus[i].GetComponent<BuildTrigger>().GetBuildMenu();
            switch (dataArray[i])
            {
                default: continue;
                case 1:
                    menus[i].GetComponent<BuildTrigger>().SetConstruction();
                    buildMenu.Build();
                    continue;
                case 2:
                    menus[i].GetComponent<BuildTrigger>().SetConstruction();
                    buildMenu.Build();
                    buildMenu.Upgrade();
                    continue;
                case 3:
                    menus[i].GetComponent<BuildTrigger>().SetConstruction();
                    buildMenu.Build();
                    buildMenu.Upgrade();
                    buildMenu.Upgrade();
                    continue;
            }
        }
    }
}