using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������Object��ۑ�����X�N���v�g
/// </summary>
[CreateAssetMenu(fileName = "bookmarkSetting.asset", menuName = "BookmarkSetting")]
public class BookmarkSetting : ScriptableObject
{
    public Bookmark[] bookmarks = new Bookmark[3];
}
[System.Serializable]
public class Bookmark{ 
    public GameObject GameObject;
}
