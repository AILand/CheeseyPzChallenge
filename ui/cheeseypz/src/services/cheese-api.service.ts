import { CHEESES_SUBPATH } from '../constants/api-subpaths';
import { CheeseModel } from '../models/cheese';
import config from '../settings.json';
import api from '../util/api';

const getCheeses = (): Promise<CheeseModel[]> => {
  return api.requests.get(`${config.apiUrl}/${CHEESES_SUBPATH}`);
};

const getCheese = (cheeseId: string): Promise<CheeseModel[]> => {
  return api.requests.get(`${config.apiUrl}/${CHEESES_SUBPATH}/${cheeseId}`);
};

const upsertCheese = (body: FormData): Promise<any> => {
  return api.requests.postForm(`${config.apiUrl}/${CHEESES_SUBPATH}`, body);
};

const deleteCheese = (id: string): Promise<any> => {
  return api.requests.del(`${config.apiUrl}/${CHEESES_SUBPATH}/${id}`);
};

const cheesesApiService = {
  getCheeses,
  getCheese,
  upsertCheese,
  deleteCheese,
};
export default cheesesApiService;
