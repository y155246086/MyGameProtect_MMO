--主入口函数。从这里开始lua逻辑
require "Logic/Game"
function Main()					
	 		LuaFramework.Util.Log("2222222222222222222222222222222222222222222222"); 
			--Game:OnInitOK();
end

--场景切换通知
function OnLevelWasLoaded(level)
	Time.timeSinceLevelLoad = 0
end