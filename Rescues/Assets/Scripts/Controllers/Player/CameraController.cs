using UnityEngine;


namespace Rescues
{
    public sealed class CameraController : IExecuteController
    {
        #region Fields

        private readonly GameContext _context;
        private readonly CameraServices _cameraServices;
        private float _distance;              
        
        #endregion


        #region ClassLifeCycles

        public CameraController(GameContext context, Services services)
        {
            _context = context;
            _cameraServices = services.CameraServices;
            _distance = Data.CameraData.Distance;
                      
        }

        #endregion


        #region IExecuteController

        public void Execute()
        {
            AttachedCamera();
            if(_cameraServices.IsCameraFree) _cameraServices.MoveCameraWithMouse();
        }

        #endregion IExecuteController


        #region Methods

        public void AttachedCamera()
        {           
            _cameraServices.CameraMain.transform.position = new Vector3(_context.Character.Transform.position.x,
               _context.Character.Transform.position.y, _context.Character.Transform.position.z - _distance);         
        }
      
        #endregion Methods
    }
}
