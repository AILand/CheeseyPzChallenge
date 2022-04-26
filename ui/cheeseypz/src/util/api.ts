import { toast } from 'react-toastify';

import axios, { AxiosResponse } from 'axios';
import { v4 as uuid } from 'uuid';

axios.interceptors.response.use(undefined, error => {
  if (!error || (!!error && error === {})) {
    return;
  } else {
    console.log('Error: ' + JSON.stringify(error));
  }

  if (error.message === 'Network Error' && !error.response) {
    toast.error('Network error!');
  }

  throw error.response;
});

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string, body: any = {}) => axios.get(url, body).then(responseBody),
  post: (url: string, body: any) => axios.post(url, body).then(responseBody),
  put: (url: string, body: any) => axios.put(url, body).then(responseBody),
  del: (url: string) => axios.delete(url).then(responseBody),
  postForm: (url: string, formData: FormData) => {
    return axios
      .post(url, formData, {
        headers: { 'Content-Type': 'multipart/form-data' },
      })
      .then(responseBody);
  },
  postFormImage: (url: string, file: Blob) => {
    const formData = new FormData();
    const fileName = `${uuid()}.jpeg`;
    formData.append('File', file, fileName);
    return axios
      .post(url, formData, {
        headers: { 'Content-type': 'multipart/form-data' },
      })
      .then(responseBody);
  },
};

const api = {
  requests,
};
export default api;
