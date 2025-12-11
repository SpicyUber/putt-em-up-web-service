import { createContext } from "react";
import type { LoginAnswer } from "../types/LoginAnswer";

export interface UserContextType {
  user: LoginAnswer;
  updateUser: (newUser: LoginAnswer) => void;
}

const defaultUser: LoginAnswer = { playerID: BigInt(-1), token: "", username: "" };
const defaultFunction = (_: LoginAnswer) => {};

const UserContext = createContext<UserContextType>({
  user: defaultUser,
  updateUser: defaultFunction,
});

export default UserContext;

