using System.IO;
using UnityEngine;


namespace Rescues
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public sealed class Data : ScriptableObject
    {
        #region Fields

        [SerializeField] private string _playerDataPath;
        [SerializeField] private string _itemDataPath;
		[SerializeField] private string _cameraDataPath;
        [SerializeField] private string _hidingPlaceDataPath;
        
        private static Data _instance;
        private static PlayerData _playerData;
        private static ItemData _itemData;
        private static HidingPlaceData _hidingPlaceData;            
        private static CameraData _cameraData;

        #endregion
        

        #region Properties

        private static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Data>("Data/" + typeof(Data).Name);
                }

                return _instance;
            }
        }
    

        public static PlayerData PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = Load<PlayerData>("Data/" + Instance._playerDataPath);
                }

                return _playerData;
            }
        }
    

        public static ItemData ItemData
        {
            get
            {
                if (_itemData == null)
                {
                    _itemData = Load<ItemData>("Data/" + Instance._itemDataPath);
                }

                return _itemData;
            }
        }

        public static HidingPlaceData HidingPlaceData
        {
            get
            {
                if (_hidingPlaceData == null)
                {
                    _hidingPlaceData = Load<HidingPlaceData>("Data/" + Instance._hidingPlaceDataPath);
                }

                return _hidingPlaceData;
            }
        }

        public static CameraData CameraData
        {
            get
            {
                if(_cameraData == null)
                {
                    _cameraData = Load<CameraData>("Data/" + Instance._cameraDataPath);
                }

                return _cameraData;
            }

        }

        #endregion


        #region Methods

        private static T Load<T>(string resourcesPath) where T : Object =>
            Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    
        #endregion
    }
}
