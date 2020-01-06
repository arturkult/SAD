import { CardRoom } from "./card-room.model";

export class Card {
  id: string;
  serialNumber: string;
  cardOwnerId: string;
  allowedRooms: CardRoom[];
}
