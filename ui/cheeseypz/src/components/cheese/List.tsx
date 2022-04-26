import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

import { CheeseModel } from '../../models/cheese';
import paths from '../../routing/paths';
import cheesesApiService from '../../services/cheese-api.service';
import { formHelper } from '../../util/form-helper';

import styles from './styles.module.scss';

interface IProps {
  cheeses: CheeseModel[];
}

const List: React.FC<IProps> = props => {
  const [cheeses, setCheeses] = useState<CheeseModel[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    setCheeses(props.cheeses);
  }, [props.cheeses]);

  const deleteOnClick = (id: string) => () => {
    if (window.confirm(`Are you sure you want to delete?`)) {
      cheesesApiService.deleteCheese(id).then(() => {
        setCheeses(cheeses.filter((cheese: CheeseModel) => cheese.id !== id));
      });
    }
  };

  const editOnClick = (id: string) => () => {
    navigate(`${paths.CHEESE}/${id.toString()}`);
  };

  const addOnClick = () => () => {
    if (cheeses.length >= 5) {
      toast.error('You can only have 5 cheeses');
    } else {
      navigate(`${paths.CHEESE_CREATE}`);
    }
  };

  useEffect(() => {
    // rerender
  }, [cheeses]);

  return (
    <>
      <div className={styles.title}>List Cheeses</div>
      <button onClick={addOnClick()} className={styles.addCheeseButton}>
        Add Cheese
      </button>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Color</th>
            <th>Price per Kg</th>
            <th>Image</th>
            <th className={styles.actionsColumn}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {cheeses.map((cheese, index) => (
            <tr key={cheese.id}>
              <td>{index + 1}</td>
              <td>{cheese.name}</td>
              <td>{cheese.color}</td>
              <td>{formHelper.formatNumberToDollars(cheese.pricePerKg, '$', '$0')}</td>
              <td>
                {!!cheese.image && <img alt="cheese pic" src={`data:image/jpeg;base64,${cheese.image}`} className={styles.cheeseImage} />}
              </td>
              <td className={styles.actionsColumn}>
                <button onClick={editOnClick(cheese.id)} className={styles.actionButton}>
                  Edit
                </button>
                <button onClick={deleteOnClick(cheese.id)} className={styles.actionButton}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};

export default List;
