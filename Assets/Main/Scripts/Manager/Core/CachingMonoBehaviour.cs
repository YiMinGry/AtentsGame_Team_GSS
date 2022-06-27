using UnityEngine;
using System.Collections;

public class CachingMonoBehaviour : MonoBehaviour
{
	GameObject _cachedObject = null;
	Transform _cachedTr = null;

	public GameObject mCashedObject { get { if (_cachedObject == null) _cachedObject = gameObject; return _cachedObject; } }
	public Transform mCashedTrans { get { if (_cachedTr == null) _cachedTr = transform; return _cachedTr; } }
	public bool activeSelf { get { return mCashedObject.activeSelf; } }
	public bool activeInHierarchy { get { return mCashedObject.activeInHierarchy; } }

	virtual protected void OnDestroy()
	{
        _cachedObject = null;
        _cachedTr = null;
	}

	virtual public void SetActive(bool bActive)
	{
		if (bActive != IsActive())
            mCashedObject.SetActive(bActive);
	}

    virtual public bool IsActive()
    {
        return activeSelf;
    }

	public T AddComponent<T>() where T : Component
	{
		return mCashedObject.AddComponent<T>();
	}

	public Transform FindChild(string strName)
	{
		return mCashedTrans.Find(strName);
	}

	public GameObject FindChildGameObject(string strName)
	{
		Transform trChild = FindChild(strName);
		return (trChild == null) ? null : trChild.gameObject;
	}

#region Func_SetParent
	public void SetParent(Transform trParent)
	{
		Transform trThis = mCashedTrans;
		if (trThis.parent != trParent)
		{
			SetParent(trParent, trThis);
		}
	}

	public void SetParent(Transform trParent, Transform trChild)
	{
		//trChild.SetParent(trParent, false);
		trChild.parent = trParent;
		trChild.localPosition = Vector3.zero;
		trChild.localRotation = Quaternion.identity;
		trChild.localScale = Vector3.one;
	}

	public void SetParent(Transform trParent, float x, float y, float z)
	{
		Transform trThis = mCashedTrans;
		trThis.parent = trParent;
		trThis.localPosition = new Vector3(x, y, z);
		trThis.localRotation = Quaternion.identity;
		trThis.localScale = Vector3.one;
	}

	public void SetParent(Transform trParent, bool bShow)
	{
		SetParent(trParent);
		SetActive(bShow);
	}
#endregion // Func_SetParent

	public void SetPositionX(float x)
	{
		Transform trThis = mCashedTrans;

		Vector3 pos = trThis.localPosition;
		pos.x = x;
		trThis.localPosition = pos;
	}

	public void SetPositionY(float y)
	{
		Transform trThis = mCashedTrans;

		Vector3 pos = trThis.localPosition;
		pos.y = y;
		trThis.localPosition = pos;
	}

	public void SetPositionXY(float x, float y)
	{
		Transform trThis = mCashedTrans;

		Vector3 pos = trThis.localPosition;
		pos.x = x;
		pos.y = y;
		trThis.localPosition = pos;
	}

	public void SetPosition(float x, float y, float z)
	{
        mCashedTrans.localPosition = new Vector3(x, y, z);
	}
}