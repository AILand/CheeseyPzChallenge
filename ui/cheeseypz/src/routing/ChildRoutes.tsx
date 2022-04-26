import React from 'react';
import { Routes, Route } from 'react-router-dom';

import App from '../App';
import CheesePage from '../components/cheese';
import Create from '../components/cheese/Create';
import EditPage from '../components/cheese/EditPage';
import NotFound from '../layout/NotFound';

import paths from './paths';

const AppChildRoutes: React.FC = () => (
  <Routes>
    <Route path="/" element={<App />} />
    <Route path={paths.CHEESE_LIST} element={<CheesePage />} />
    <Route path={paths.CHEESE + '/:id'} element={<EditPage />} />
    <Route path={paths.CHEESE_CREATE} element={<Create />} />

    {/* default */}
    <Route element={<NotFound />} />
  </Routes>
);

export default AppChildRoutes;
