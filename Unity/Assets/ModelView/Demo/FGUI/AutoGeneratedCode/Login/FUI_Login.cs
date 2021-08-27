/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using System.Threading.Tasks;

namespace ET
{
    public class FUI_LoginAwakeSystem : AwakeSystem<FUI_Login, GObject>
    {
        public override void Awake(FUI_Login self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Login : FUI
    {	
        public const string UIPackageName = "Login";
        public const string UIResName = "Login";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    	public Controller m_Gro_ShowVersionInfo;
    	public GButton m_Btn_Login;
    	public GButton m_Btn_Registe;
    	public GImage m_accountInput;
    	public GImage m_passwordInput;
    	public GTextInput m_accountText;
    	public GTextField m_Tex_LoginInfo;
    	public GButton m_ToTestSceneBtn;
    	public GTextInput m_passwordText;
    	public GGroup m_Gro_LoginInfo;
    	public Transition m_t0;
    	public Transition m_t1;
    	public const string URL = "ui://2jxt4hn8pdjl9";

       
        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }
    
        private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
        {
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
        }
        
       
        public static FUI_Login CreateInstance(Entity domain)
        {			
            return EntityFactory.Create<FUI_Login, GObject>(domain, CreateGObject());
        }
        
       
        public static ETTask<FUI_Login> CreateInstanceAsync(Entity domain)
        {
            ETTask<FUI_Login> tcs = ETTask<FUI_Login>.Create(true);
    
            CreateGObjectAsync((go) =>
            {
                tcs.SetResult(EntityFactory.Create<FUI_Login, GObject>(domain, go));
            });
    
            return tcs;
        }
        
       
        public static FUI_Login Create(Entity domain, GObject go)
        {
            return EntityFactory.Create<FUI_Login, GObject>(domain, go);
        }
            
       
        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static FUI_Login GetFormPool(Entity domain, GObject go)
        {
            var fui = go.Get<FUI_Login>();
        
            if(fui == null)
            {
                fui = Create(domain, go);
            }
        
            fui.isFromFGUIPool = true;
        
            return fui;
        }
            
        public void Awake(GObject go)
        {
            if(go == null)
            {
                return;
            }
            
            GObject = go;	
            
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = Id.ToString();
            }
            
            self = (GComponent)go;
            
            self.Add(this);
            
            var com = go.asCom;
                
            if(com != null)
            {	
                
    			m_Gro_ShowVersionInfo = com.GetControllerAt(0);
    			m_Btn_Login = (GButton)com.GetChildAt(3);
    			m_Btn_Registe = (GButton)com.GetChildAt(4);
    			m_accountInput = (GImage)com.GetChildAt(5);
    			m_passwordInput = (GImage)com.GetChildAt(6);
    			m_accountText = (GTextInput)com.GetChildAt(7);
    			m_Tex_LoginInfo = (GTextField)com.GetChildAt(8);
    			m_ToTestSceneBtn = (GButton)com.GetChildAt(9);
    			m_passwordText = (GTextInput)com.GetChildAt(10);
    			m_Gro_LoginInfo = (GGroup)com.GetChildAt(11);
    			m_t0 = com.GetTransitionAt(0);
    			m_t1 = com.GetTransitionAt(1);
    		}
    	}
           
        public override void Dispose()
        {
            if(IsDisposed)
            {
                return;
            }
            
            base.Dispose();
            
            self.Remove();
            self = null;
            
    		m_Gro_ShowVersionInfo = null;
    		m_Btn_Login = null;
    		m_Btn_Registe = null;
    		m_accountInput = null;
    		m_passwordInput = null;
    		m_accountText = null;
    		m_Tex_LoginInfo = null;
    		m_ToTestSceneBtn = null;
    		m_passwordText = null;
    		m_Gro_LoginInfo = null;
    		m_t0 = null;
    		m_t1 = null;
    	}
    }
}