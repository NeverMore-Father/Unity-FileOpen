using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 文件日志类
/// </summary>
// [特性(布局种类.有序,字符集=字符集.自动)]
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class ChinarFileDlog
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileDlg : ChinarFileDlog
{
}

public class OpenFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileDlg ofd);
}

public class SaveFileDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] SaveFileDlg ofd);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class SaveFileDlg : ChinarFileDlog
{
}


public class AppEnter : MonoBehaviour {

    public Text text;
    public RawImage Img;

	// Use this for initialization
	void Start () {
		
	}


    IEnumerator LoadImage(string filename)
    {
        WWW wwwTexture = new WWW("file://" + filename);
        Debug.Log(wwwTexture.url);
        yield return wwwTexture;
        Img.texture = wwwTexture.texture;
    }

    // Update is called once per frame
    void Update () {

        transform.Rotate(Vector3.up, 1);

        if (Input.GetKeyDown(KeyCode.T))
        {
            OpenFileDlg pth = new OpenFileDlg();
            pth.structSize = Marshal.SizeOf(pth);
            //pth.filter = "三菱(*.gxw)\0*.gxw\0西门子(*.mwp)\0*.mwp\0All Files\0*.*\0\0";
            pth.filter = "图片(*.png;*jpg)\0*.png;*.jpg";
            pth.file = new string(new char[256]);
            pth.maxFile = pth.file.Length;
            pth.fileTitle = new string(new char[64]);
            pth.maxFileTitle = pth.fileTitle.Length;
            //pth.initialDir = Application.dataPath.Replace("/", "\\") + "\\Resources"; //默认路径
            pth.title = "选择图片";
            pth.defExt = "txt";
            pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
            Debug.LogError(1);
            if (OpenFileDialog.GetOpenFileName(pth))
            {
                text.text = pth.file;
                StartCoroutine(LoadImage(pth.file));
            }
            else
                text.text = "取消操作";
        }
	}
}
