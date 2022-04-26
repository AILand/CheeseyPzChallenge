import React from 'react';
import { useParams } from 'react-router-dom';

import Edit from './Edit';

const EditPage: React.FC = () => {
  const match = { params: useParams() };

  return <Edit id={match.params.id}></Edit>;
};

export default EditPage;
