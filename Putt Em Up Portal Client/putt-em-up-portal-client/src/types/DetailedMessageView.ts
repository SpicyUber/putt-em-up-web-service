import type { Message } from "./Message";
import type { Profile } from "./Profile";

export interface DetailedMessageView{
    message:Message
    from:Profile
    to:Profile
}