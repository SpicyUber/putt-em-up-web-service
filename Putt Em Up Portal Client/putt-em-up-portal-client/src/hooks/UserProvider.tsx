import { useState } from "react";
import type { LoginAnswer } from "../types/LoginAnswer";
import UserContext from "./UserContext";
import type { UserContextType } from "./UserContext";



const UserProvider = ({ children } : React.PropsWithChildren) => {

  const defaultUser:LoginAnswer={playerID: BigInt(-1),token:"",username:""}

  const [user, setUser] = useState<LoginAnswer>(defaultUser );

  const updateUser = (newUser: LoginAnswer) => {
    setUser(newUser);
  };

  const userContext : UserContextType = {user:user,updateUser:updateUser}

  return (
    <UserContext.Provider value={userContext}>
      {children}
    </UserContext.Provider>
  );
};

export default UserProvider;
