import type { Profile } from "./Profile";

export interface LeaderboardPage{

    profiles: Profile[],
    totalPages: number
}