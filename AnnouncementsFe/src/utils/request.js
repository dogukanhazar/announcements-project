import axios from 'axios';

export const urls = {
  BASE_URL: 'https://localhost:5001',
  API_PATH: '/api/v1',
  ANNOUNCEMENTS_PATH: '/Announcements',
  GET_ALL_QUERY: '?get_all=',
  PAGE_QUERY: '?page='
};

export const request = (requestObject = {}) => {
  requestObject.baseURL = requestObject?.baseURL || urls.BASE_URL + urls.API_PATH;

  requestObject.timeout = requestObject?.timeout || 2000;

  requestObject.headers = {
    ...axios?.defaults?.headers?.common,
    ...requestObject?.headers
  };

  axios?.defaults?.headers[requestObject?.method] &&
    (requestObject.headers = {
      ...axios?.defaults?.headers[requestObject?.method],
      ...requestObject?.headers
    });

  requestObject.transformResponse = [
    (data, headers) => {
      let parsed = '';
      try {
        parsed = JSON.parse(data);
      } catch (error) {
        parsed = { error: 'json parse error' };
      }
      return parsed;
    }
  ];

  return axios(requestObject);
};
