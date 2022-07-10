using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class UnitSkinManager : MonoBehaviour
{
    [SerializeField]
    Transform root;

    public enum skinType
    {
        Human_1, Human_2, Human_3, Human_4, Human_5, Elf_1, Elf_2, Orc_1, Orc_2, Orc_3, Orc_4, Skelton_1, Devil_1,
        None
    }
    public enum eyeType
    {
        Eye0, Eye1, Eye2, Eye3, Eye4, Eye5, Eye6, Eye7, Eye8, Eye9, Eye10,
        Eye11, Eye12, Eye13, Eye14, Eye15, Eye16, Eye17, Eye18, Eye19, Eye20,
        Eye21, Eye22, Eye23, Eye24, Eye25, Eye26, Eye27, Eye28, Eye29, Eye30,
        Eye31, Eye32, Eye33, Eye34, Eye35, Eye36, Eye37, Eye38, Eye39, Eye40,
        Eye41, Eye42, Eye43, Eye44, Eye45, Eye46, Eye47
    }

    public enum hairType
    {
        None, Hair_1, Hair_2, Hair_3, Hair_4, Hair_5, Hair_6, Hair_7, Hair_8, Hair_9, New_Hair_01,
        New_Hair_02, New_Hair_03, New_Hair_04, New_Hair_05, New_Hair_06, New_Hair_07, New_Hair_08,
        New_Hair_09, New_Hair_10, New_Hair_11, New_Hair_12, New_Hair_13, New_Hair_14, New_Hair_15,
        New_Hair_16, New_Hair_17, New_Hair_18, New_Hair_19, New_Hair_20, Normal_Hair1, Normal_Hair10,
        Normal_Hair11, Normal_Hair12, Normal_Hair13, Normal_Hair2, Normal_Hair3, Normal_Hair4,
        Normal_Hair5, Normal_Hair6, Normal_Hair7, Normal_Hair8, Normal_Hair9
    }
    public enum faceHairType
    {
        None, FaceHair_1, FaceHair_2, FaceHair_3, FaceHair_4, FaceHair_5, Normal_Mustache1, Normal_Mustache2
    }
    public enum clothType
    {
        None, Cloth_1, Cloth_2, Cloth_3, Cloth_4, Cloth_5, Cloth_6, Cloth_7, Cloth_8, Cloth_9,
        Cloth_10, Cloth_11, Cloth_12, New_Cloth_01, New_Cloth_02, New_Cloth_03, New_Cloth_04,
        New_Cloth_05, New_Cloth_06, New_Cloth_07, New_Cloth_08, New_Cloth_09, New_Cloth_10,
        New_Cloth_11, New_Cloth_12, Normal_Cloth1
    }
    public enum pantType
    {
        None, Foot_1, Foot_2, Foot_3, Foot_4, New_Pant_01, New_Pant_02, New_Pant_03, New_Pant_04, New_Pant_05,
        New_Pant_06, New_Pant_07, New_Pant_08, New_Pant_09, New_Pant_10, New_Pant_11, New_Pant_12
    }
    public enum helmetType
    {
        None, Helmet_1, Helmet_2, Helmet_3, Helmet_4, Helmet_5, Helmet_6, Helmet_7, Helmet_8, Helmet_9,
        New_Helmet_01, New_Helmet_02, New_Helmet_03, New_Helmet_04, New_Helmet_05, New_Helmet_06,
        New_Helmet_07, New_Helmet_08, New_Helmet_09, New_Helmet_10, Normal_Helmet1, Normal_Helmet2, Normal_Helmet3
    }
    public enum armorType
    {
        None, Armor_1, Armor_2, Armor_3, Armor_4, Armor_5, Armor_6, Armor_7, Armor_8, New_Armor_01,
        New_Armor_02, New_Armor_03, New_Armor_04, New_Armor_05, New_Armor_06, New_Armor_07,
        New_Armor_08, New_Armor_09, New_Armor_10, New_Armor_11, New_Armor_12, Normal_Armor1
    }
    public enum weaponsType
    {
        None, Axe_1, AxeLong1, AxeNormal1, AxeSmall1, Bow_1, New_Weapon_01, New_Weapon_02, New_Weapon_03,
        New_Weapon_04, New_Weapon_05, New_Weapon_06, New_Weapon_07, New_Weapon_08, New_Weapon_09,
        New_Weapon_10, New_Weapon_11, New_Weapon_12, New_Weapon_13, New_Weapon_14, New_Weapon_15,
        New_Weapon_16, New_Weapon_17, New_Weapon_18, New_Weapon_19, New_Weapon_20, Soon_Spear, Spear_1,
        Sword_1, Sword_2, Sword_3, Sword_4, Sword_5, Sword_6, Ward_1
    }
    public enum shieldType
    {
        None, New_Shield_01, New_Shield_02, New_Shield_03, New_Shield_04, Shield_1, SteelShield1, WoodShield1, WoodShield2, WoodShield3, WoodShield4
    }
    public enum backType
    {
        None, Back_1, Back_2, Back_3, BowBack_1, New_Back_01, New_Back_02, Soon_Back1
    }

    [System.Serializable]
    struct bodyInfo
    {
        public SpriteRenderer armL;
        public SpriteRenderer armR;
        public SpriteRenderer cArmL;
        public SpriteRenderer cArmR;
        public SpriteRenderer shoulderL;
        public SpriteRenderer shoulderR;
        public SpriteRenderer body;
        public SpriteRenderer footL;
        public SpriteRenderer footR;
        public SpriteRenderer head;

    }
    [System.Serializable]
    struct eyeInfo
    {
        public SpriteRenderer[] back;
        public SpriteRenderer[] front;
    }

    [System.Serializable]
    struct hairInfo
    {
        public SpriteRenderer hair;
    }
    [System.Serializable]
    struct faceHairInfo
    {
        public SpriteRenderer faceHair;
    }
    [System.Serializable]
    struct clothInfo
    {
        public SpriteRenderer body;
        public SpriteRenderer left;
        public SpriteRenderer right;

    }
    [System.Serializable]
    struct pantInfo
    {
        public SpriteRenderer left;
        public SpriteRenderer right;
    }
    [System.Serializable]
    struct helmetInfo
    {
        public SpriteRenderer helmet;
        public SpriteRenderer helmet_2;

    }
    [System.Serializable]
    struct armorInfo
    {
        public SpriteRenderer body;
        public SpriteRenderer left;
        public SpriteRenderer right;
    }
    [System.Serializable]
    struct weaponsInfo
    {
        public SpriteRenderer left;
        public SpriteRenderer right;
    }
    [System.Serializable]
    struct shieldInfo
    {
        public SpriteRenderer left;
        public SpriteRenderer right;
    }

    [System.Serializable]
    struct backInfo
    {
        public SpriteRenderer back;
    }
    [System.Serializable]
    struct shadowInfo
    {
        public SpriteRenderer shadow;
    }

    int _skinIdx = 0;
    int _eyeIdx = 0;
    int _hairIdx = 0;
    int _faceHairIdx = 0;
    int _clothIdx = 0;
    int _pantIdx = 0;
    int _helmetIdx = 0;
    int _armorIdx = 0;
    int _weaponsIdx_L = 0;
    int _shieldIdx_L = 0;
    int _weaponsIdx_R = 0;
    int _shieldIdx_R = 0;
    int _backIdx = 0;

#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string skinName = "";
    [Range(0, 12)]
    public int _skinType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string eyeName = "";
    [Range(0, 47)]
    public int _eyeType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string eyeColorCode = "";
    public Color eyeColors;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string hairName = "";
    [Range(0, 42)]
    public int _hairType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string faceHairName = "";
    [Range(0, 7)]
    public int _faceHairType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string clothName = "";
    [Range(0, 25)]
    public int _clothType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string pantName = "";
    [Range(0, 16)]
    public int _pantType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string helmetName = "";
    [Range(0, 22)]
    public int _helmetType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string armorName = "";
    [Range(0, 21)]
    public int _armorType = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string weaponsName_L = "";
    [Range(0, 34)]
    public int _weaponsType_L = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string weaponsName_R = "";
    [Range(0, 34)]
    public int _weaponsType_R = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string shieldName_L = "";
    [Range(0, 10)]
    public int _shieldType_L = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string shieldName_R = "";
    [Range(0, 10)]
    public int _shieldType_R = 0;
#if UNITY_EDITOR
    [ReadOnlyAttribute]
#endif
    public string backName = "";
    [Range(0, 7)]
    public int _backType = 0;

    //[SerializeField]
    bodyInfo _body;
    //[SerializeField]
    eyeInfo _eye;
    //[SerializeField]
    hairInfo _hairInfo;
    //[SerializeField]
    faceHairInfo _faceHairInfo;
    //[SerializeField]
    clothInfo _clothInfo;
    //[SerializeField]
    pantInfo _pantInfo;
    //[SerializeField]
    helmetInfo _helmetInfo;
    //[SerializeField]
    armorInfo _armorInfo;
    //[SerializeField]
    weaponsInfo _weaponsInfo;
    //[SerializeField]
    shieldInfo _shieldInfo;
    //[SerializeField]
    backInfo _backInfo;
    //[SerializeField]
    shadowInfo _shadowInfo;

    //public List<SpriteRenderer> pppppp = new List<SpriteRenderer>();

    private void Awake()
    {
        SpriteRenderer[] srArr = root.GetComponentsInChildren<SpriteRenderer>();

        _backInfo.back = srArr[0];
        _body.body = srArr[1];
        _clothInfo.body = srArr[2];
        _armorInfo.body = srArr[3];
        _hairInfo.hair = srArr[4];
        _body.head = srArr[5];
        _faceHairInfo.faceHair = srArr[6];
        _eye.front = new SpriteRenderer[2];
        _eye.back = new SpriteRenderer[2];
        _eye.back[0] = srArr[7];
        _eye.front[0] = srArr[8];
        _eye.back[1] = srArr[9];
        _eye.front[1] = srArr[10];
        _helmetInfo.helmet = srArr[11];
        _helmetInfo.helmet_2 = srArr[12];
        _body.armL = srArr[13];
        _clothInfo.left = srArr[14];
        _armorInfo.left = srArr[15];
        _body.cArmL = srArr[16];
        _body.shoulderL = srArr[17];
        _weaponsInfo.left = srArr[18];
        _shieldInfo.left = srArr[19];
        _body.armR = srArr[20];
        _clothInfo.right = srArr[21];
        _armorInfo.right = srArr[22];
        _body.cArmR = srArr[23];
        _body.shoulderR = srArr[24];
        _weaponsInfo.right = srArr[25];
        _shieldInfo.right = srArr[26];
        _body.footR = srArr[27];
        _pantInfo.right = srArr[28];
        _body.footL = srArr[29];
        _pantInfo.left = srArr[30];
        _shadowInfo.shadow = srArr[31];
    }

    private void Update()
    {
        if (_skinIdx != (int)_skinType)
        {
            _skinIdx = (int)_skinType;
            SkinChange();
        }

        if (_eyeIdx != (int)_eyeType)
        {
            _eyeIdx = (int)_eyeType;

            eyeChange();
        }

        if (_hairIdx != (int)_hairType)
        {
            _hairIdx = (int)_hairType;

            hairChange();
        }

        if (_faceHairIdx != (int)_faceHairType)
        {
            _faceHairIdx = (int)_faceHairType;

            faceHairChange();
        }

        if (_clothIdx != (int)_clothType)
        {
            _clothIdx = (int)_clothType;

            clothChange();
        }

        if (_pantIdx != (int)_pantType)
        {
            _pantIdx = (int)_pantType;

            pantChange();
        }

        if (_helmetIdx != (int)_helmetType)
        {
            _helmetIdx = (int)_helmetType;

            helmetChange();
        }

        if (_armorIdx != (int)_armorType)
        {
            _armorIdx = (int)_armorType;

            armorChange();
        }

        if (_weaponsIdx_L != (int)_weaponsType_L)
        {
            _weaponsIdx_L = (int)_weaponsType_L;

            weapons_L_Change();
        }

        if (_weaponsIdx_R != (int)_weaponsType_R)
        {
            _weaponsIdx_R = (int)_weaponsType_R;

            weapons_R_Change();
        }

        if (_shieldIdx_L != (int)_shieldType_L)
        {
            _shieldIdx_L = (int)_shieldType_L;

            shield_L_Change();
        }

        if (_shieldIdx_R != (int)_shieldType_R)
        {
            _shieldIdx_R = (int)_shieldType_R;

            shield_R_Change();
        }
        if (_backIdx != (int)_backType)
        {
            _backIdx = (int)_backType;

            backChange();
        }

        if (eyeColorCode != eyeColors.ToString())
        {
            eyeColorCode = eyeColors.ToString();
            _eye.front[0].color = eyeColors;
            _eye.front[1].color = eyeColors;
        }
    }

    void SkinChange()
    {
        skinName = ((skinType)_skinType).ToString();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/BodySource/Body/" + (skinType)_skinType);

        _body.armL.sprite = sprites[0];
        _body.armR.sprite = sprites[1];
        _body.body.sprite = sprites[2];
        _body.footL.sprite = sprites[3];
        _body.footR.sprite = sprites[4];
        _body.head.sprite = sprites[5];

    }

    void eyeChange()
    {
        eyeName = ((eyeType)_eyeType).ToString();

        //if (_eyeType == 0)
        //{
        //    _eye.back[0].sprite = null;
        //    _eye.back[1].sprite = null;
        //    _eye.front[0].sprite = null;
        //    _eye.front[1].sprite = null;

        //    return;
        //}

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/BodySource/Eye/" + (eyeType)_eyeType);

        _eye.back[0].sprite = sprites[0];
        _eye.back[1].sprite = sprites[0];
        _eye.front[0].sprite = sprites[1];
        _eye.front[1].sprite = sprites[1];
    }
    void hairChange()
    {
        hairName = ((hairType)_hairType).ToString();

        if (_hairType == 0)
        {
            _hairInfo.hair.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/0_Hair/" + (hairType)_hairType);
        _hairInfo.hair.sprite = sprites[0];

    }
    void faceHairChange()
    {
        faceHairName = ((faceHairType)_faceHairType).ToString();

        if (_faceHairType == 0)
        {
            _faceHairInfo.faceHair.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/1_FaceHair/" + (faceHairType)_faceHairType);
        _faceHairInfo.faceHair.sprite = sprites[0];

    }
    void clothChange()
    {
        clothName = ((clothType)_clothType).ToString();

        if (_clothType == 0)
        {
            _clothInfo.body.sprite = null;
            _clothInfo.left.sprite = null;
            _clothInfo.right.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/2_Cloth/" + (clothType)_clothType);

        Dictionary<string, Sprite> tmpSp = new Dictionary<string, Sprite>();
        tmpSp.Clear();

        for (int i = 0; i < sprites.Length; i++)
        {
            tmpSp.Add(sprites[i].name, sprites[i]);
        }

        if (tmpSp.ContainsKey("Body"))
        {
            _clothInfo.body.sprite = tmpSp["Body"];
        }
        if (tmpSp.ContainsKey("Left"))
        {
            _clothInfo.left.sprite = tmpSp["Left"];
        }
        if (tmpSp.ContainsKey("Right"))
        {
            _clothInfo.right.sprite = tmpSp["Right"];
        }
    }
    void pantChange()
    {
        pantName = ((pantType)_pantType).ToString();

        if (_pantType == 0)
        {
            _pantInfo.left.sprite = null;
            _pantInfo.right.sprite = null;
            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/3_Pant/" + (pantType)_pantType);

        Dictionary<string, Sprite> tmpSp = new Dictionary<string, Sprite>();
        tmpSp.Clear();

        for (int i = 0; i < sprites.Length; i++)
        {
            tmpSp.Add(sprites[i].name, sprites[i]);
        }

        if (tmpSp.ContainsKey("Left"))
        {
            _pantInfo.left.sprite = tmpSp["Left"];
        }
        if (tmpSp.ContainsKey("Right"))
        {
            _pantInfo.right.sprite = tmpSp["Right"];
        }
    }
    void helmetChange()
    {
        helmetName = ((helmetType)_helmetType).ToString();

        if (_helmetType == 0)
        {
            _hairInfo.hair.enabled = true;
            _helmetInfo.helmet.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/4_Helmet/" + (helmetType)_helmetType);
        _helmetInfo.helmet.sprite = sprites[0];
        _hairInfo.hair.enabled = false;
    }
    void armorChange()
    {
        armorName = ((armorType)_armorType).ToString();

        if (_armorType == 0)
        {
            _clothInfo.body.enabled = true;
            _clothInfo.left.enabled = true;
            _clothInfo.right.enabled = true;

            _armorInfo.body.sprite = null;
            _armorInfo.left.sprite = null;
            _armorInfo.right.sprite = null;

            return;
        }

        _clothInfo.body.enabled = false;
        _clothInfo.left.enabled = false;
        _clothInfo.right.enabled = false;

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/5_Armor/" + (armorType)_armorType);

        Dictionary<string, Sprite> tmpSp = new Dictionary<string, Sprite>();
        tmpSp.Clear();

        for (int i = 0; i < sprites.Length; i++)
        {
            tmpSp.Add(sprites[i].name, sprites[i]);
        }

        if (tmpSp.ContainsKey("Body"))
        {
            _armorInfo.body.sprite = tmpSp["Body"];
        }
        if (tmpSp.ContainsKey("Left"))
        {
            _armorInfo.left.sprite = tmpSp["Left"];
        }
        if (tmpSp.ContainsKey("Right"))
        {
            _armorInfo.right.sprite = tmpSp["Right"];
        }
    }
    void weapons_L_Change()
    {
        weaponsName_L = ((weaponsType)_weaponsType_L).ToString();

        if (_weaponsType_L == 0)
        {
            _weaponsInfo.left.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/6_Weapons/" + (weaponsType)_weaponsType_L);
        _weaponsInfo.left.sprite = sprites[0];
    }
    void weapons_R_Change()
    {
        weaponsName_R = ((weaponsType)_weaponsType_R).ToString();

        if (_weaponsType_R == 0)
        {
            _weaponsInfo.right.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/6_Weapons/" + (weaponsType)_weaponsType_R);
        _weaponsInfo.right.sprite = sprites[0];
    }
    void shield_L_Change()
    {
        shieldName_L = ((shieldType)_shieldType_L).ToString();

        if (_shieldType_L == 0)
        {
            _shieldInfo.left.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/6_Weapons/" + (shieldType)_shieldType_L);
        _shieldInfo.left.sprite = sprites[0];
    }
    void shield_R_Change()
    {
        shieldName_R = ((shieldType)_shieldType_R).ToString();

        if (_shieldType_R == 0)
        {
            _shieldInfo.right.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/6_Weapons/" + (shieldType)_shieldType_R);
        _shieldInfo.right.sprite = sprites[0];
    }
    void backChange()
    {
        backName = ((backType)_backType).ToString();

        if (_backType == 0)
        {
            _backInfo.back.sprite = null;

            return;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Cha/Units_Sprites/Items/7_Back/" + (backType)_backType);
        _backInfo.back.sprite = sprites[0];
    }

    public void RandomSet()
    {
        _skinType = Random.Range(0, 12);
        _eyeType = Random.Range(0, 47);
        _hairType = Random.Range(0, 42);
        _faceHairType = Random.Range(0, 7);
        _clothType = Random.Range(0, 25);
        _pantType = Random.Range(0, 16);
        _helmetType = Random.Range(0, 22);
        _armorType = Random.Range(0, 21);
    }
}
