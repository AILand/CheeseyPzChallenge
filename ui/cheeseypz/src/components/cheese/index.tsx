import React, { useEffect, useState } from 'react';

import { CheeseModel } from '../../models/cheese';
import cheesesApiService from '../../services/cheese-api.service';

import List from './List';

const CheesePage: React.FC = () => {
  const [cheeses, setCheeses] = useState<CheeseModel[]>([]);

  useEffect(() => {
    cheesesApiService.getCheeses().then((data: any) => setCheeses(data));
  }, []);

  return <List cheeses={cheeses}></List>;
};

export default CheesePage;
