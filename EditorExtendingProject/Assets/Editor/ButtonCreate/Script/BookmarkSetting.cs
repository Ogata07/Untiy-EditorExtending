using UnityEngine;
/// <summary>
/// 生成させるObjectを保存するスクリプト
/// </summary>
[CreateAssetMenu(fileName = "bookmarkSetting.asset", menuName = "BookmarkSetting")]
public class BookmarkSetting : ScriptableObject
{
    public Bookmark[] bookmarks = new Bookmark[3];
}
[System.Serializable]
public class Bookmark{ 
    public GameObject saveObject;
}
