export interface Message {
  fromPlayerID: BigInt;
  toPlayerID: BigInt;
  sentTimestamp: string;
  reported: boolean;
  content: string;
}
